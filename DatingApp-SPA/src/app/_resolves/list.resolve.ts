import { catchError } from 'rxjs/operators';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { UserService } from './../_Services/user.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { of, Observable } from 'rxjs';

@Injectable()


export class ListResolve implements Resolve<User[]> {
  pageNumber = 1 ;
  pageSize = 15;
  likeParem = 'likers';
  constructor(private userserivce: UserService , private router: Router, private alert: AlertifyjsService) {
  }

resolve(): Observable<User[]> {
 return this.userserivce.getUsers(this.pageNumber, this.pageSize, null, this.likeParem).pipe(catchError(error => {
    this.alert.error('Faild Get Like Users');
    this.router.navigate(['/Home']);
    return of(null);
  }));

}
}
