import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Customer } from '../customer.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CustomerService } from '../customer.service';
import { Observable } from 'rxjs';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';


@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.css']
})
export class CustomerListPageComponent implements OnInit, AfterViewInit {
  customers$!: Observable<Customer[]>;

  dataSource!: MatTableDataSource<Customer>; // The ! tells Angular you know it may be used before it is set.  Try it without to see the error
  displayColumns = ['icon', 'name', 'phoneNumber', 'emailAddress', 'statusCode', 'lastContactDate', 'edit'];



  constructor(
    private customerService: CustomerService,
    private router: Router,
    public dialog: MatDialog
    ) {
    this.customers$ = this.customerService.search('');
  }

  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void { }
  openDetail(item: Customer): void {
    if(item) {
      this.router.navigate([`./customer/${item.customerId}`])
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
