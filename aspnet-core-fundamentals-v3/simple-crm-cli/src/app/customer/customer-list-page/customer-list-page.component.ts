import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Customer } from '../customer.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CustomerService } from '../customer.service';
import { Observable } from 'rxjs';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.css']
})
export class CustomerListPageComponent implements OnInit, AfterViewInit {
  customers$!: Observable<Customer[]>;

  dataSource!: MatTableDataSource<Customer>; // The ! tells Angular you know it may be used before it is set.  Try it without to see the error
  displayColumns = ['name', 'phoneNumber', 'emailAddress', 'statusCode'];



  constructor(private customerService: CustomerService, public dialog: MatDialog) {
    this.customers$ = this.customerService.search('');
  }

  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void { }

  ngAfterViewInit() {
    // this.dataSource.sort = this.sort;
  }

  addCustomer(){
    const dialogRef = this.dialog.open(CustomerCreateDialogComponent, {
      width: '10rem',
      data: null
    });
  }

}
