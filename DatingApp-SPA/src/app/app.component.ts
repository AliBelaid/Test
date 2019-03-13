import { AuthService } from './_Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  JWTHelper = new JwtHelperService();
  constructor(private authserver: AuthService) { }
  ngOnInit() {

    const token = localStorage.getItem('token');
    if (token) {
      this.authserver.TokenDecoder = this.JWTHelper.decodeToken(token);
    }
    const user = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.authserver.userCurrency = user;
      this.authserver.changeMemberPhoto(user.photoUrl);
    }
  }
}
