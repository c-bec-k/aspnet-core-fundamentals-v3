import { PlatformLocation } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { anonymousUser, GoogleOptions, MicrosoftOptions, UserSummaryViewModel } from './account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl: string;
  private cachedUser = new BehaviorSubject<UserSummaryViewModel>(anonymousUser());

  constructor(
    private http: HttpClient,
    private router: Router,
    private platformLocation: PlatformLocation,
    private snackBar: MatSnackBar
    ) {
      this.baseUrl = `${environment.server}${environment.apiUrl}auth`;
      const cu = localStorage.getItem('currentUser');
      if (cu) {
        const curUser = JSON.parse(cu);
        this.cachedUser.next(curUser);
        this.verifyUser(curUser).subscribe({next: (user) => this.setUser(user)});
      }
     }

     get user(): BehaviorSubject<UserSummaryViewModel> {
       return this.cachedUser;
     }

    setUser(user: UserSummaryViewModel): void {
      this.cachedUser.next(user);
      localStorage.setItem('currentUser', JSON.stringify(user));
    }

    public loginMicrosoftOptions(): Observable<MicrosoftOptions> {

      return this.http.get<MicrosoftOptions>(`${this.baseUrl}/external/microsoft`);
    }

    // Maybe come back and do Google later? Maybe?
    //
    // public loginGoogleOptions(): Observable<GoogleOptions> {
    //   return this.http.get<GoogleOptions>(`${this.baseUrl}external/google`);
    // }

    get isAnonymous(): boolean {
      if (this.cachedUser.value.name === 'Anonymous') return true;
      return false;
    }

    loginComplete(user: UserSummaryViewModel, _message: string) {
      this.setUser(user);
    }

    logout(options: { navigate: boolean } = {navigate: true }): void {
      this.cachedUser.next(anonymousUser());
      localStorage.removeItem('currentUser');
      if (options && options.navigate) {
        // window.location.href = this.envronmentServices.environment.server;
      }

    }

    loginMicrosoft(code: string, state: string): Observable<UserSummaryViewModel> {
      const body = { accessToken: code, state, baseHref: this.platformLocation.getBaseHrefFromDOM() };
      return this.http.post<UserSummaryViewModel>(`${this.baseUrl}/external/microsoft`, body);
    }

    verifyUser(user: UserSummaryViewModel): Observable<UserSummaryViewModel> {
      const model = {};
      const options = !user || !user.jwt ? {} :
            { headers: {Authorization: `Bearer ${user.jwt}` }};
      return this.http.post<UserSummaryViewModel>(
        `${this.baseUrl}/verify`,
        model,
        options
      )
    }

}
