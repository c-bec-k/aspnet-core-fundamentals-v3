import { Injectable } from '@angular/core';
import { Actions } from '@ngrx/effects';
import { CustomerService } from '../customer/customer.service';

@Injectable()

export class CustomerStoreEffects {
  constructor( private actions$: Actions, private custSvc: CustomerService) {

  }
}
