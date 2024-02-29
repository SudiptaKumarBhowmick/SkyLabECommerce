import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { adminUserLogin } from '../../_models/adminUserLogin';
import { AccountService } from '../../_services/account.service';
import { SingleResponse } from '../../_responses/singleResponse';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.css'
})
export class AdminLoginComponent {
  @ViewChild("loginForm") loginForm!: NgForm;

  constructor(
    private accountService: AccountService,
    private _toastr: ToastrService,
    private router: Router){}

  adminLoginFormsModel: adminUserLogin = {
    id: 0,
    userName: '',
    password: '',
    userEmail: '',
    userTypeId: 0,
    typeName: '',
    token: ''
  }

  login(){
    this.accountService.adminLogin(this.adminLoginFormsModel).subscribe(result => {
      var response = result as SingleResponse<adminUserLogin>;
      if(response.statusCode == 200){
        sessionStorage.setItem('admin', JSON.stringify(response.model));
        this.accountService.changeAdminUserLoginStatus(true);
        this.router.navigate(['/admin']);
        //this.userName = response.model.userName;
        this._toastr.success("Login Successful");
        //this.modalService.closeModal();
        this.resetForms();
      }
      else{
        this._toastr.error(response.message);
      }
    })
  }

    // ------------Reset Form--------------
    resetForms(){
        this.loginForm.reset({
          email: "",
          password: ""
        });
    }
}
