import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs';

//If admin user logged in then prevent accessing at admin panel
export const adminAuthGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  return accountService.isAdminUserLoggedIn$.pipe(
    map(adminUser => {
      if(adminUser) return true;
      else{
        router.navigate(['/admin/login']);
        return false;
      }
    })
  )
};

//If admin user logged in then prevent accessing at login page
export const adminAuthRedirectGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  return accountService.isAdminUserLoggedIn$.pipe(
    map(adminUser => {
      if(adminUser) {
        router.navigate(['/admin']);
        return false;
      }
      else{
        return true;
      }
    })
  )
};