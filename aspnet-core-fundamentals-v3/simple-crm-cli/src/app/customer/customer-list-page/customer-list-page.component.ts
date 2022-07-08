import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectionStrategy, Input } from '@angular/core';
import { Customer } from '../customer.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CustomerService } from '../customer.service';
import { combineLatest, Observable } from 'rxjs';
import { map, startWith, tap } from 'rxjs/operators';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { CustomerState } from '../store/customer.store.model';
import { selectCustomers } from '../store/customer.store.selectors';
import { searchCustomersAction } from '../store/customer.store';

@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})


export class CustomerListPageComponent implements OnInit, AfterViewInit {
  customers$!: Observable<Customer[]>;
  filteredCustomers$!: Observable<Customer[]>;
  filterInput = new FormControl();
  dataSource!: MatTableDataSource<Customer>; // The ! tells Angular you know it may be used before it is set.  Try it without to see the error


  constructor(
    private customerService: CustomerService,
    public dialog: MatDialog,
    private store: Store<CustomerState>,
    ) {
    this.customers$ = this.store.pipe(select(selectCustomers));
    this.store.dispatch(searchCustomersAction({criteria: {term: ""}}));
    this.filteredCustomers$ = combineLatest([this.customers$, this.filterInput.valueChanges.pipe(startWith(""))]).pipe(
      map(([customers, filter])=>{
       return customers.filter((cust)=>{
         return (`${cust.firstName} ${cust.lastName}`.toLowerCase().includes(filter.toLowerCase()) ||
              cust.emailAddress.toLowerCase().includes(filter.toLowerCase()) ||
              cust.phoneNumber.includes(filter)
            )
        });
      })
    );
  }

  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void { }

  ngAfterViewInit() {
    // this.dataSource.sort = this.sort;
  }

  addCustomer(){
    const dialogRef = this.dialog.open(CustomerCreateDialogComponent, {
      width: '15rem',
      data: null
    });

    dialogRef.afterClosed().subscribe((customer: Customer) =>
    {
      if (customer === undefined) {return;}
      this.customerService.insert(customer).subscribe( e => {
        this.store.dispatch(searchCustomersAction({criteria: {term: ""}}));
      });
    });
  }
}
