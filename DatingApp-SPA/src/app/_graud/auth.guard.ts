import { AlertifyjsService } from './../_Services/alertifyjs.service';
import { AuthService } from './../_Services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor( private auth: AuthService , private route: Router , private alert: AlertifyjsService) {

  }
  canActivate(): boolean {
    if (this.auth.LoggenIn()) {
      return true ;
      }
      this.alert.error(' your Not Connect!!');
      this.route.navigate(['/Home']);
    return false;
  }
}
