import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { CustomerModule } from './customer/customer.module';
import { AppIconsService } from './app-icons.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { AccountRoutingModule } from './account/account-routing.module';
import { JwtIntecptorInterceptor } from './account/jwt-intecptor.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { layoutFeatureKey, layoutReducer } from './store/layout.store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AccountRoutingModule,
    BrowserAnimationsModule,
    CustomerModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatDatepickerModule,
    StoreModule.forRoot({}),
    StoreModule.forFeature(layoutFeatureKey, layoutReducer),
    StoreDevtoolsModule.instrument({
      name: 'Nexul Academy — SimpleCRM'
    }),
    EffectsModule.forRoot([])
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtIntecptorInterceptor, multi: true}, AppIconsService],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor(iconService: AppIconsService) {}
 }
