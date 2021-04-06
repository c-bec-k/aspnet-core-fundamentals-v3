import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MatSortModule } from '@angular/material/sort';
import { CustomerService } from './customer.service';
import { HttpClientModule } from '@angular/common/http';
import { CustomerMockService } from './customer-mock.service';
import { FlexLayoutModule}  from '@angular/flex-layout';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CustomerCreateDialogComponent } from './customer-create-dialog/customer-create-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [CustomerListPageComponent, CustomerCreateDialogComponent],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    HttpClientModule,
    MatCardModule,
    MatTableModule,
    MatSortModule,
    FlexLayoutModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule
  ],
  providers: [
    {
      provide: CustomerService,
      useClass: CustomerMockService

    }]
})
export class CustomerModule { }
