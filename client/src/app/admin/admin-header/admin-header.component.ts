import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-header',
  standalone: true,
  imports: [],
  templateUrl: './admin-header.component.html',
  styleUrl: './admin-header.component.css'
})
export class AdminHeaderComponent {
  userName?: string = "";

  constructor(
    private _toastr: ToastrService,
    private accountService: AccountService,
    private router: Router){}

  ngOnInit(){
    this.getAdminInformationFromSession();
  }

  getAdminInformationFromSession(){
    var adminUserSession = sessionStorage.getItem('admin');
    if(adminUserSession != null){
      var adminUserSessionData = JSON.parse(adminUserSession);
      this.userName = adminUserSessionData.userName;
    }
  }

  Logout(){
    sessionStorage.removeItem('admin');
    this.accountService.changeAdminUserLoginStatus(false);
    this.router.navigate(['admin/login']);
    this._toastr.success("Logout Successful");
  }
}
