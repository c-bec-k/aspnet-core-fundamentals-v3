import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { CustomerMockService } from './customer-mock.service';

describe('CustomerMockService', () => {
  let injector: TestBed;
  let service: CustomerMockService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [ CustomerMockService ]
    });
      injector = getTestBed();
      service = injector.inject(CustomerMockService);
      httpMock = injector.inject(HttpTestingController);
    });

    afterEach( () => {
      httpMock.verify();
    });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
