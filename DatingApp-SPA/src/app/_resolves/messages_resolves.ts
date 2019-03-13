import { AuthService } from './../_Services/auth.service';
import { catchError } from 'rxjs/operators';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { UserService } from '../_Services/user.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Message } from '../_models/Message';

@Injectable()

export class MessagesResolve implements Resolve<Message[]> {
  pageNumber = 1;
  pageSize = 10;
  messageContainer = 'UnRead';
  constructor(private userserivce: UserService, private auth: AuthService, private router: Router, private alert: AlertifyjsService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
    return this.userserivce.getMessage(this.auth.TokenDecoder.nameid
      , this.pageNumber, this.pageSize, this.messageContainer).pipe(catchError
        (error => {
        this.alert.error('Problem retrieving messsages');
        this.router.navigate(['/Home']);
        return of(null);
      }));

  }
}
