import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { switchMap } from 'rxjs';
import { CustomerService } from '../customer/customer.service';

@Injectable()

export class CustomerStoreEffects {
  constructor( private actions$: Actions, private custSvc: CustomerService) { }

  searchCustomers$ = createEffect( () => this.actions$.pipe(
    ofType(searchCustomersAction),
    switchMap({criteria}) => this.custSvc.search(criteria.term).pipe(
      map(
        data => searchCustomersCompleteAction({result: data});
      )
    )
  ));
}
