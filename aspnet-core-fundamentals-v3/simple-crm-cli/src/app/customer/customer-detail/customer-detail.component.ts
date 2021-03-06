import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {MatDatepickerModule} from '@angular/material/datepicker';

@Component({
  selector: 'crm-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {
  customerId!: number;
  customer!: Customer;
  detailForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
    ) {
      this.createForm();
     }

     createForm(): void {
       this.detailForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: [''],
        emailAddress: ['', [Validators.required, Validators.email]],
        preferredContactMethod: ['email'],
        statusCode: [''],
        lastContactDate: ['']
       })
     }



    cancel() { this.router.navigate([`/customers/`]) }

  ngOnInit(): void {
    this.customerId = parseInt(this.route.snapshot.params.id, 10)
    this.customerService.get(this.customerId).subscribe(cust => {if (cust) {
      this.customer = cust;
      if (!this.customer.lastContactDate) {
        this.customer.lastContactDate = new Date().toISOString();
      }
      this.detailForm.patchValue(cust);
    }
    });

  }
  save(): void {
    if (!this.detailForm.valid) return;
    const customer = { ...this.customer, ...this.detailForm.value };
    this.customerService.update(customer).subscribe( () => {
      this.snackBar.open('Customer Saved', 'OK');
      this.router.navigate([`/customers/`]);
    });
  }
}
