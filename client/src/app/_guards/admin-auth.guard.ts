import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs';

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