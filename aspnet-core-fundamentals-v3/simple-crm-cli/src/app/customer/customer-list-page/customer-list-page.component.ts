import { Component, OnInit } from '@angular/core';
import { Customer } from '../customer.modle';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.css']
})
export class CustomerListPageComponent implements OnInit {
  customers: Customer[] = [
    {
      customerId: 1,
      firstName: 'John',
      lastName: 'Smith',
      phoneNumber: '314-555-1234',
      emailAddress: 'john@nexulacademy.com',
      statusCode: 'Prospect',
      preferredContactMethod: 'phone',
      lastContactDate: new Date().toISOString()
    },
    {
      customerId: 2,
      firstName: 'Tory',
      lastName: 'Amos',
      phoneNumber: '314-555-9873',
      emailAddress: 'tory@example.com',
      statusCode: 'Prospect',
      preferredContactMethod: 'email',
      lastContactDate: new Date().toISOString()
    }
  ];

  dataSource!: MatTableDataSource<Customer>; // The ! tells Angular you know it may be used before it is set.  Try it without to see the error
  displayColumns = ['name', 'phone', 'email', 'status'];



  constructor() { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.customers);
  }


}