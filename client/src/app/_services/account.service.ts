import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { userLogin } from '../_models/userLogin';
import { environment } from '../environments/environment.dev';
import { userRegistration } from '../_models/userRegistration';
import { BehaviorSubject } from 'rxjs';
import { adminUserLogin } from '../_models/adminUserLogin';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private isUserLoggedIn = new BehaviorSubject<boolean>(false);
  isUserLoggedIn$ = this.isUserLoggedIn.asObservable();

  private isAdminUserLoggedIn = new BehaviorSubject<boolean>(false);
  isAdminUserLoggedIn$ = this.isAdminUserLoggedIn.asObservable();

  constructor(private http: HttpClient) { }

  userLogin(data: userLogin){
    return this.http.post(environment.apiUrl + "Account/login", data);
  }

  userRegistration(data: userRegistration){
    return this.http.post(environment.apiUrl + "Account/register", data);
  }

  adminLogin(data: adminUserLogin){
    return this.http.post(environment.apiUrl + "Account/admin/login", data);
  }

  changeUserLoginStatus(isLoggedIn: boolean){
    if(isLoggedIn){
      this.isUserLoggedIn.next(true);
    }
    else{
      this.isUserLoggedIn.next(false);
    }
  }

  changeAdminUserLoginStatus(isLoggedIn: boolean){
    if(isLoggedIn){
      this.isAdminUserLoggedIn.next(true);
    }
    else{
      this.isAdminUserLoggedIn.next(false);
    }
  }
}
