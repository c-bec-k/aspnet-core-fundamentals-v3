import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserSummaryViewModel } from './account.model';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticatedGuard implements CanActivate {
  constructor(
    private router: Router,
    private accountService: AccountService,
  ){}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      localStorage.setItem('loginReturnUrl', state.url);

      // TODO: inject your accountService in the constructor, you'll create it in the next section

      return this.accountService.user.pipe(
        map((user: UserSummaryViewModel) => {
          if (user.name === 'Anonymous') {
            return this.router.createUrlTree(['./login']);
          };

          return true;
        }),
      );
  }

}
