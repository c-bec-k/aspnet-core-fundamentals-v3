import { PlatformLocation } from '@angular/common';
import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from '../account.service';

@Component({
  selector: 'crm-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginType: 'undecided' | 'password' | 'microsoft' | 'google' = 'undecided';

  constructor(
    private accountService: AccountService,
    private platformLocation: PlatformLocation,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
  }

  useMicrosoft(): void {
    this.loginType = 'microsoft';
    this.snackBar.open('Signing In with Microsoft...', '', { duration: 2000 });
    const baseUrl =
      'https://login.microsoftonline.com/common/oauth2/v2.0/authorize?';
    this.accountService.loginMicrosoftOptions().subscribe((opts) => {
      const options: { [key: string]: string } = {
        ...opts,
        response_type: 'code',
        redirect_uri:
          window.location.origin +
          this.platformLocation.getBaseHrefFromDOM() +
          'account/signin-microsoft',
      };
      console.log(options.redirect_uri);
      let params = new HttpParams();
      for (const key of Object.keys(options)) {
        params = params.set(key, options[key]); // encodes values automatically.
      }

      window.location.href = baseUrl + params.toString();
    });
  }

}
