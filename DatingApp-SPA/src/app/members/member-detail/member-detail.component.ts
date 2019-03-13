import { Photo } from './../../_models/Photo';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_Services/user.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  @ViewChild('staticTabs') staticTabs: TabsetComponent;


  constructor(private UserServer: UserService , private alrart: AlertifyjsService, private route: ActivatedRoute) { }

  ngOnInit() {
   this.route.data.subscribe(data => {
    this.user = data['user'];
  });
  this.route.queryParams.subscribe(param => {
    const selecttab = param['tab'];
    this.staticTabs.tabs[selecttab > 0 ?  selecttab : 1].active = true;
  });
  this.galleryOptions = [
    {
        width: '500px',
        height: '500px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide
    }];
   this.galleryImages = this.GetImgaes();
  }


GetImgaes() {
 const photoUrl = [];
for ( let i = 0 ; i < this.user.photos.length; i++) {
  photoUrl.push({
    small: this.user.photos[i].url,
    medium:  this.user.photos[i].url,
    big:  this.user.photos[i].url,
    description:  this.user.photos[i].description
  });

}
return photoUrl;
}
// GetDeatil() {
// this.UserServer.getUser(this.route.snapshot.params['id']).subscribe(response => {
//   this.user = response ;
//   console.log(response);
// }, error => {this.alrart.error(error); } );
// }

selectTab(tabId: number) {
  this.staticTabs.tabs[tabId].active = true;
}
}
