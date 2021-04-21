import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'crm-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {
  customerId!: number;
  customer!: Customer;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.customerId = parseInt(this.route.snapshot.params.id, 10)
    this.customerService.get(this.customerId).subscribe(cust => {if (cust) this.customer = cust});
  }

}
