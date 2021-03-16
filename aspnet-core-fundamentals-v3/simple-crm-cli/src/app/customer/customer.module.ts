import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MatSortModule } from '@angular/material/sort';
import { CustomerService } from './customer.service';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [CustomerListPageComponent],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    HttpClientModule,
    MatCardModule,
    MatTableModule,
    MatSortModule
  ],
  providers: [CustomerService]
})
export class CustomerModule { }
