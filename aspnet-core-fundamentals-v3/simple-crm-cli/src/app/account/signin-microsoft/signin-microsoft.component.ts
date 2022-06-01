import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';



@Component({
  selector: 'crm-signin-microsoft',
  templateUrl: './signin-microsoft.component.html',
  styleUrls: ['./signin-microsoft.component.css']
})
export class SigninMicrosoftComponent {
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    public snackBar: MatSnackBar
  ) {
    this.route.queryParamMap.subscribe(prms => {
      const code = prms.get("code") || "";
      const sessionState = prms.get("session_state") || "";
      if (code) {
        this.snackBar.open("Validating login…", "", {duration: 8000});
        this.loading = true;
        this.accountService.loginMicrosoft(code, sessionState).subscribe(
          (result) => {
            this.accountService.loginComplete(result, 'Email has been verified');
            this.router.navigate(["customers"]);
          },
          (__) => {
            // error state
            this.loading = false;
            this.accountService.logout({ navigate: false});
            this.snackBar.open("Verification failed. Try to login with another account.", "", {duration: 10000});
            this.router.navigate(["./login"])
          }
        )


      }
    });
  }

}
