import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
    catchError(error => {
      if (error instanceof HttpErrorResponse) {
        const applicationError = error.headers.get('Application-Error');
        if (error.status === 401) {

          return throwError(error.statusText);
        }
        if (applicationError) {
          console.log(applicationError);
          return throwError(applicationError);
        }
       const serverError = error.error;
       // tslint:disable-next-line:prefer-const
       let ModelStateError = '';
        if (serverError != null && typeof serverError === 'object') {
for (let key = 0; key < serverError.length; key++) {
   if (serverError[key]) {
     ModelStateError += serverError[key] + '\n';
   }
}
        }
        return throwError(serverError || ModelStateError || 'server is Off');
      }
    }
     ));
  }
}
export const ErrorInterceptorProvider = {
provide: HTTP_INTERCEPTORS ,
useClass :  ErrorInterceptor ,
multi : true
};
