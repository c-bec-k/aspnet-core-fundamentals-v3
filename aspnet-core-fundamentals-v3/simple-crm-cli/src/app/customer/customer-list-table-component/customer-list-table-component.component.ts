import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from '../customer.model';

@Component({
  selector: 'crm-customer-list-table-component',
  templateUrl: './customer-list-table-component.component.html',
  styleUrls: ['./customer-list-table-component.component.css']
})
export class CustomerListTableComponentComponent implements OnInit {
  displayColumns = ['icon', 'name', 'phoneNumber', 'emailAddress', 'statusCode', 'lastContactDate', 'edit'];

  constructor(
    private router: Router,
  ) { }
  @Input() customers: Customer[] | null | undefined

  ngOnInit(): void {
  }
  openDetail(item: Customer): void {
    if(item) {
      this.router.navigate([`./customers/${item.id}`])
    }
  }

  trackByUserId = (idx: number, item: Customer) => item.id
}
