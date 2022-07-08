import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerListTableComponentComponent } from './customer-list-table-component.component';

describe('CustomerListTableComponentComponent', () => {
  let component: CustomerListTableComponentComponent;
  let fixture: ComponentFixture<CustomerListTableComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerListTableComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerListTableComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
