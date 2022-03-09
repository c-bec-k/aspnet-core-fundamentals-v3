import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticatedGuard } from '../account/authenticated.guard';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';

const routes: Routes = [
  {
    path: 'customers',
    canActivate: [AuthenticatedGuard],
    pathMatch: 'full',
    component: CustomerListPageComponent
  },
  {
    path: 'customer/:id',
    canActivate: [AuthenticatedGuard],
    pathMatch: 'full',
    component: CustomerDetailComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
