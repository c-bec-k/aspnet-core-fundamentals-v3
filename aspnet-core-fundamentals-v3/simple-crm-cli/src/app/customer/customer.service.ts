import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from './customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  search(term: string): Observable<Customer[]> {
    return this.http.get<Customer[]>(`/api/customer/?term=${term}`);
  }

  insert(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>('/api/customer/', customer);
  }

  update(customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(`api/customer/${customer.id}`, customer);
  }

  get(customerId: number): Observable<Customer | undefined> {
    return this.http.get<Customer>(`/api/customer/${customerId}`);
  }

}
