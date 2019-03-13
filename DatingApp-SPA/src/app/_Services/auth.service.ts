import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import { JwtHelperService} from '@auth0/angular-jwt';
import { User } from '../_models/User';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
UrlPath = 'http://localhost:5000/api/Auth/';
JWTHelper = new JwtHelperService();
TokenDecoder: any;
userCurrency: User;
photoMain = new BehaviorSubject<string>('../../assets/userIcon.png');
userMainPhoto = this.photoMain.asObservable();
constructor(private http: HttpClient) { }

changeMemberPhoto(photoUrl: string) {
this.photoMain.next(photoUrl);
}

Login(model: any) {
return  this.http.post(this.UrlPath + 'Login', model)
.pipe(map((response: any ) => {
   const user = response;
  if (user)  {
   localStorage.setItem('token', user.token);
  localStorage.setItem('user', JSON.stringify(user.userToReturen));
this.userCurrency = user.userToReturen ;
this.changeMemberPhoto(this.userCurrency.photoUrl);
 this.TokenDecoder = this.JWTHelper.decodeToken(user.token);
 console.log(this.userCurrency);

  }
}));
}
Register(model: any) {
return  this.http.post(this.UrlPath + 'Register', model);
}
LoggenIn() {
const token = localStorage.getItem('token');
return !this.JWTHelper.isTokenExpired(token);
}

}
