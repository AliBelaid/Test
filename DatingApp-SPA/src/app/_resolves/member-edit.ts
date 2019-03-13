import { AuthService } from './../_Services/auth.service';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_Services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()


export class MemberEditResolve implements Resolve<User> {
constructor(private userservice: UserService , private routre: Router, private authuser: AuthService
 , private alrt: AlertifyjsService) {
 }
resolve(): Observable<User> {
return this.userservice.getUser(this.authuser.TokenDecoder.nameid).pipe(catchError(error => {
this.alrt.error('Problam retrieving data');
this.routre.navigate(['/members']);
return of(null);
}));
}


}
