import { AuthService } from './../../_Services/auth.service';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { ActivatedRoute } from '@angular/router';
import { User } from './../../_models/User';
import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_Services/user.service';
import { token } from 'src/app/app.module';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editFrom') editFrom: NgForm;
  user: User;
  mainPhoto: string;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editFrom.dirty) {
      $event.returnValue = true;
    }
  }
  constructor(private route: ActivatedRoute, private alert: AlertifyjsService,
    private authServices: AuthService, private userSerivce: UserService) { }
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
    this.authServices.userMainPhoto.subscribe(photo => {
      this.mainPhoto = photo;
    });
  }
  updateUsers() {
    console.log(this.user.userName);
    this.userSerivce.updateUser(this.authServices.TokenDecoder.nameid, this.user).subscribe(next => {
      this.alert.success('User Update Success!');
      this.editFrom.reset(this.user);
    }, error => {
      this.alert.error(error);
    });
  }
  loaderMainPhoto(urlPhoto) {
    this.user.photoUrl = urlPhoto;
    this.authServices.changeMemberPhoto(urlPhoto);
  }
}
