import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Customer } from './customer.model';
import { CustomerService } from './customer.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerMockService extends CustomerService {

  customers: Customer[] = [];

  constructor(http: HttpClient) {
    super(http);

    const localCustomers = localStorage.getItem('customers');
  if (localCustomers) {
    console.log('reading localCustomers storage')
    this.customers = JSON.parse(localCustomers);
  } else {
    console.log('No localCustomers storage')
    this.customers.push({
      customerId: 1,
      firstName: 'John',
      lastName: 'Smith',
      phoneNumber: '314-555-1234',
      emailAddress: 'john@nexulacademy.com',
      statusCode: 'prospect',
      preferredContactMethod: 'phone',
      lastContactDate: new Date().toISOString()
    });
  }
  }

  search(term: string): Observable<Customer[]> {
    if (!term) return of(this.customers);
    const results = this.customers.filter(c => (c.firstName.indexOf(term) > 0 ||
    c.lastName.indexOf(term) > 0 ||
    c.phoneNumber.indexOf(term) > 0 ||
    c.emailAddress.indexOf(term) > 0) ||
    (`${c.firstName} ${c.lastName}`).indexOf(term) > 0);
    return of(results);
  }

  insert(customer: Customer): Observable<Customer> {
    customer.customerId = Math.max(...this.customers.map( customer => customer.customerId)) +1;
    this.customers = [...this.customers, customer];
    localStorage.setItem('customers', JSON.stringify(this.customers));
    return of(customer);
  }

  update(customer: Customer): Observable<Customer> {
    const foundCustomer = this.customers.filter(c => c.customerId === customer.customerId);
    if(foundCustomer) {
      this.customers = this.customers.map(c => c.customerId === customer.customerId ? customer : c);
    } else {
      this.customers = [...this.customers, customer];
    }
    localStorage.setItem('customers', JSON.stringify(this.customers));
    return of(customer);
  }

  get(customerId: number): Observable<Customer | undefined> {
    const item = this.customers.find(c => c.customerId === customerId);
    return of(item);
  }

}
