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
        this.cachedUser.next(JSON.parse(cu));
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
      // TODO: Add interface for MicrosoftOptions to account.model.ts
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
}
