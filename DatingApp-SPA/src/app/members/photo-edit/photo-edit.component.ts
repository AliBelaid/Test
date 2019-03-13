import { AlertifyjsService } from './../../_Services/alertifyjs.service';
import { UserService } from './../../_Services/user.service';
import { AuthService } from './../../_Services/auth.service';
import { environment } from './../../../environments/environment';
import { Photo } from './../../_models/Photo';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
@Component({
  selector: 'app-photo-edit',
  templateUrl: './photo-edit.component.html',
  styleUrls: ['./photo-edit.component.css']
})
export class PhotoEditComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() urlPhoto = new EventEmitter<string>();

  baseUrl = environment.apiUrl;
  public uploader: FileUploader;
  hasBaseDropZoneOver = false;
  hasAnotherDropZoneOver = false;
  currenvyPhotoMain: Photo;
  constructor(private authservice: AuthService, private alertify: AlertifyjsService, private userService: UserService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'user/' + this.authservice.TokenDecoder.nameid + '/photo',
      isHTML5: true,
      removeAfterUpload: true,
      authToken: 'Bearer ' + localStorage.getItem('token'),
      autoUpload: false,
      allowedFileType: ['image'],
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = (File) => {
      File.withCredentials = false;
    };
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      const res: Photo = JSON.parse(response);
      const photo = {
        id: res.id,
        url: res.url,
        description: res.description,
        knownAs: res.knownAs,
        isMain: res.isMain,
        dateAdd: res.dateAdd
      };
      this.photos.push(photo);
      if (photo.isMain)  {
        this.authservice.userCurrency.photoUrl = photo.url;
        this.authservice.changeMemberPhoto(photo.url);
        localStorage.setItem('user', JSON.stringify(this.authservice.userCurrency));
       }
    };

  }
  SingMainPhoto(photo: Photo) {
    this.userService.updateMainPhto(this.authservice.TokenDecoder.nameid, photo.id).subscribe(() => {
      this.alertify.success(' Scuccess add');
      this.currenvyPhotoMain = this.photos.filter(p => p.isMain === true)[0];
      this.currenvyPhotoMain.isMain = false;
      photo.isMain = true;
      this.urlPhoto.emit(photo.url);
      this.authservice.userCurrency.photoUrl = photo.url;
      this.authservice.changeMemberPhoto(photo.url);
      localStorage.setItem('user', JSON.stringify(this.authservice.userCurrency));
    }, error => {
      this.alertify.error(error);
    });
  }
DeletePhoto(id) {
  this.userService.deltePhoto(this.authservice.TokenDecoder.nameid, id).subscribe(() => {
this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
this.alertify.success('The photo has beed deleted');
} , error => { this.alertify.error(' Filed  to delete the photo'); }
) ;
}
}
