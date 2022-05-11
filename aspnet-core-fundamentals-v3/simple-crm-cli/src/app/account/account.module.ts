import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { SigninMicrosoftComponent } from './signin-microsoft/signin-microsoft.component';



@NgModule({
  declarations: [
    LoginComponent,
    SigninMicrosoftComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AccountModule { }
