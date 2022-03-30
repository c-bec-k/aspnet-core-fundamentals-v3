import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticatedGuard } from '../account/authenticated.guard';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';

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
  {
    path: 'not-authorized',
    canActivate: [AuthenticatedGuard],
    pathMatch: 'full',
    component: NotAuthorizedComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
