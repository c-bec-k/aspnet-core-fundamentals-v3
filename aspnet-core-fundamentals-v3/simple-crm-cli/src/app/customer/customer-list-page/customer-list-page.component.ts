import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
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



@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.css']
})
export class CustomerListPageComponent implements OnInit, AfterViewInit {
  customers$!: Observable<Customer[]>;
  filteredCustomers$!: Observable<Customer[]>;
  filterInput = new FormControl();
  dataSource!: MatTableDataSource<Customer>; // The ! tells Angular you know it may be used before it is set.  Try it without to see the error
  displayColumns = ['icon', 'name', 'phoneNumber', 'emailAddress', 'statusCode', 'lastContactDate', 'edit'];



  constructor(
    private customerService: CustomerService,
    private router: Router,
    public dialog: MatDialog
    ) {
    this.customers$ = this.customerService.search('');
    this.filteredCustomers$ = combineLatest([this.customers$, this.filterInput.valueChanges.pipe(startWith(""))]).pipe(
      map(([customers, filter])=>{
       return customers.filter((cust)=>{
         return (`${cust.firstName} ${cust.lastName}`.toLowerCase().includes(filter.toLowerCase()) ||
              cust.emailAddress.toLowerCase().includes(filter.toLowerCase()) ||
              cust.phoneNumber.includes(filter)
            )
        });
      })
    )
  }

  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void { }
  openDetail(item: Customer): void {
    if(item) {
      this.router.navigate([`./customers/${item.customerId}`])
    }
  }

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
        this.customers$ = this.customerService.search('');
      });
    });
  }



}
