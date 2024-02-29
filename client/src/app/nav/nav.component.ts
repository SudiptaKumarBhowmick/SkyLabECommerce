import { Component, ViewChild } from '@angular/core';
import { CustomModalComponent } from "../custom-modal/custom-modal.component";
import { CommonModule } from '@angular/common';
import { CustomModalService } from '../_services/custom-modal.service';
import { FormsModule, NgForm } from '@angular/forms';
import { userLogin } from '../_models/userLogin';
import { SingleResponse } from '../_responses/singleResponse';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';
import { userRegistration } from '../_models/userRegistration';
import { EmailPatternDirective } from '../_directives/email-pattern.directive';
import { PasswordPatternDirective } from '../_directives/password-pattern.directive';

@Component({
    selector: 'app-nav',
    standalone: true,
    templateUrl: './nav.component.html',
    styleUrl: './nav.component.css',
    imports: [
      CustomModalComponent,
      CommonModule,
      FormsModule,
      EmailPatternDirective,
      PasswordPatternDirective
    ]
})
export class NavComponent {

  constructor(
    private modalService: CustomModalService,
    private accountService: AccountService,
    private _toastr: ToastrService)
    {
    }

    ngOnInit(){
      var userSession = sessionStorage.getItem('user');
      if(userSession != null){
        this.accountService.changeUserLoginStatus(true);
        var userSessionData = JSON.parse(userSession);
        this.userName = userSessionData.userName;
      }
    }

  modalOpen$ = this.modalService.modalVisible$;
  isUserLoggedIn$ = this.accountService.isUserLoggedIn$;
  userName: string = '';

  //Modal functions for login and signup
  openModal(){
    this.modalService.openModal();
  }

  closeModal(){
    this.modalService.closeModal();
  }

  // ----------user login-------------
  @ViewChild("loginForm") loginForm!: NgForm;
  
  userLoginFormModel = {
    loginFormUserNameOrEmail: "",
    loginFormPassword: ""
  };

  userLoginDataModel: userLogin = {
    userName: "",
    password: "",
    email: "",
    token: "",
    statusCode: 0,
    message: ""
  };

  login(){
    const emailRegex = RegExp(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$/);
    const isEmail = emailRegex.test(this.userLoginFormModel.loginFormUserNameOrEmail);

    if(isEmail){
      this.userLoginDataModel.email = this.userLoginFormModel.loginFormUserNameOrEmail;
      this.userLoginDataModel.password = this.userLoginFormModel.loginFormPassword;
    }
    else{
      this.userLoginDataModel.userName = this.userLoginFormModel.loginFormUserNameOrEmail;
      this.userLoginDataModel.password = this.userLoginFormModel.loginFormPassword;
    }

    this.accountService.userLogin(this.userLoginDataModel).subscribe((result) => {
      var response = result as SingleResponse<userLogin>;
      if(response.statusCode == 200){
        sessionStorage.setItem('user', JSON.stringify(response.model));
        this.accountService.changeUserLoginStatus(true);
        this.userName = response.model.userName;
        this._toastr.success("Login Successful");
        this.modalService.closeModal();
        this.resetForms('login');
      }
      else{
        this._toastr.error(response.message);
      }
    })
  }

  // -----------user registration------------
  @ViewChild("registrationForm") registrationForm!: NgForm;

  userRegistrationFormModel: userRegistration = {
    userName: "",
    password: "",
    userEmail: "",
    mobile: "",
    token: ""
  };

  register(){
    this.accountService.userRegistration(this.userRegistrationFormModel).subscribe(result => {
      var response = result as SingleResponse<userRegistration>;
      if(response.statusCode == 200){
        sessionStorage.setItem('user', JSON.stringify(response.model));
        this.accountService.changeUserLoginStatus(true);
        this.userName = response.model.userName;
        this._toastr.success("Registration Successful");
        this.modalService.closeModal();
        this.resetForms('register');
      }
      else{
        this._toastr.error(response.message);
      }
    })
  }

  // ----------Logout------------
  Logout(){
    sessionStorage.removeItem('user');
    this.accountService.changeUserLoginStatus(false);
    this._toastr.success("Logout Successful");
  }

  // ------------Reset Form--------------
  resetForms(formType: string){
    if(formType == "login")
    {
      this.loginForm.reset({
        loginFormUserNameOrEmail: "",
        loginFormPassword: ""
      });
    }
    else if(formType == "register"){
      this.registrationForm.reset({
        registerFormUserName: "",
        registerFormPassword: "",
        registerFormEmail: "",
        registerFormMobile: ""
      });
    }
  }
}
