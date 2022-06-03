import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from './account.service';

@Injectable()
export class JwtIntecptorInterceptor implements HttpInterceptor {

  constructor(private snackbar: MatSnackBar, private accountService: AccountService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwt = this.getJwt();
    if (jwt) {
      request = request.clone({setHeaders: {Authorization: `Bearer ${jwt}`}});
    }

    return next.handle(request).pipe(
      tap(
        (event: HttpEvent<any>) => {},
        (error: any) =>{
          if (error instanceof HttpErrorResponse && error.status === 401) {
            this.snackbar.open("You're not logged in! Please log in to continue", undefined, {duration: 5000});
            this.accountService.logout();
            location.href = `/login`;
          }
        }
      )
    );
  }

  getJwt = () => {
    const user = window.localStorage.getItem('currentUser');
    if (user) {
      return JSON.parse(user).jwt;
    }
    return null;
  }
}
