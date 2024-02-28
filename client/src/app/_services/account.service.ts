import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { userLogin } from '../_models/userLogin';
import { environment } from '../environments/environment.dev';
import { userRegistration } from '../_models/userRegistration';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private isUserLoggedIn = new BehaviorSubject<boolean>(false);
  isUserLoggedIn$ = this.isUserLoggedIn.asObservable();

  constructor(private http: HttpClient) { }

  userLogin(data: userLogin){
    return this.http.post(environment.apiUrl + "Account/login", data);
  }

  userRegistration(data: userRegistration){
    return this.http.post(environment.apiUrl + "Account/register", data);
  }

  changeUserLoginStatus(isLoggedIn: boolean){
    if(isLoggedIn){
      this.isUserLoggedIn.next(true);
    }
    else{
      this.isUserLoggedIn.next(false);
    }
  }
}
