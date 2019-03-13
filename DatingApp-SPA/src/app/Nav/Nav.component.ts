import { AuthService } from '../_Services/auth.service';
import { AlertifyjsService } from './../_Services/alertifyjs.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
 @Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-Nav',
  templateUrl: './Nav.component.html',
  styleUrls: ['./Nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
photoUrl: string;
  constructor(public   auth: AuthService, private alert: AlertifyjsService , private route: Router) { }

  ngOnInit() {
   this.auth.userMainPhoto.subscribe( photo => {
     this.photoUrl = photo ;
   }) ;
  }
Login() {
this.auth.Login(this.model).subscribe(next => {
  this.alert.success('Scussed');
}, error => {
  this.alert.error(error);
 }, () => {
 this.route.navigate(['/Member']);
} )
;

}

LoggedIn() {
 return this.auth.LoggenIn() ;

}

LogOut() {
   localStorage.removeItem('token');
   localStorage.removeItem('user');
   this.auth.TokenDecoder = null;
   this.auth.userCurrency = null;
   this.alert.message('Logged Out');
   this.route.navigate(['/Home']);
  }
}
