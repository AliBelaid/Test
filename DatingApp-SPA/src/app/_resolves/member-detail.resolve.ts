import { catchError } from 'rxjs/operators';
import { AlertifyjsService } from './../_Services/alertifyjs.service';
import { UserService } from 'src/app/_Services/user.service';
import { Injectable } from '@angular/core';
import { User } from './../_models/User';
import { Resolve, RouterModule, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable()

export class MemberDetailResolve implements Resolve<User> {

constructor(private userservires: UserService, private alert: AlertifyjsService
  , private route: Router) {}
resolve(rout: ActivatedRouteSnapshot): Observable<User> {


 return this.userservires.getUser(rout.params['id']).pipe(catchError(
   error => {
  this.alert.error('Problam retrieving data');
  this.route.navigate(['/Home']);
    return of(null); }
  ));
}}
