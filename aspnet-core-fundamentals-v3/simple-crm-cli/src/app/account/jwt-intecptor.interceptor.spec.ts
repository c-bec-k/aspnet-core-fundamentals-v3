import { TestBed } from '@angular/core/testing';

import { JwtIntecptorInterceptor } from './jwt-intecptor.interceptor';

describe('JwtIntecptorInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      JwtIntecptorInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: JwtIntecptorInterceptor = TestBed.inject(JwtIntecptorInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
