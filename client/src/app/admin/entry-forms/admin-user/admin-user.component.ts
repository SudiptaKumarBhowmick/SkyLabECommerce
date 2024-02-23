import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from "@angular/forms";
import { adminUser } from '../../../_models/adminUser';
import { GenericService } from '../../../_services/generic.service';
import { ListResponse } from '../../../_responses/listResponse';
import { CommonModule } from '@angular/common';
import { userType } from '../../../_models/userType';
import { SingleResponse } from '../../../_responses/singleResponse';
import { EmailPatternDirective } from '../../../_directives/email-pattern.directive';
import { PasswordPatternDirective } from '../../../_directives/password-pattern.directive';
import { ValidateSelectOptionDirective } from '../../../_directives/validate-select-option.directive';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-user',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    EmailPatternDirective,
    PasswordPatternDirective,
    ValidateSelectOptionDirective
  ],
  templateUrl: './admin-user.component.html',
  styleUrl: './admin-user.component.css'
})
export class AdminUserComponent {
  @ViewChild("adminUserAddForm") adminUserAddForm!: NgForm;
  @ViewChild("adminUserEditForm") adminUserEditForm!: NgForm;

  adminUserList: adminUser[] = [];
  userTypeListAddForm: userType[] = [];
  userTypeListEditForm: userType[] = [];

  addAdminUserModel: adminUser = {
    id: 0,
    password: "",
    userEmail: "",
    userName: "",
    userTypeId: -1,
    userType: {
      id: 0,
      typeName: ""
    }
  };

  editAdminUserModel: adminUser = {
    id: 0,
    password: "",
    userEmail: "",
    userName: "",
    userTypeId: -1,
    userType: {
      id: 0,
      typeName: ""
    }
  };

  @ViewChild("adminUserEditFormCloseButton") adminUserEditFormCloseButton!: ElementRef;

  constructor(private genericService: GenericService, private _toastr: ToastrService){

  }

  ngOnInit(){
    this.getAllUserTypes();
    this.getAllAdminUser();
  }

  getAllAdminUser(){
    this.genericService.controllerName = "AdminUser";
    this.genericService.getAll().subscribe(adminUsers => {
      var response = adminUsers as ListResponse<adminUser>;
      this.adminUserList = response.model;
    })
  }

  getAllUserTypes(){
    this.genericService.controllerName = "UserType";
    this.genericService.getAll().subscribe(userTypes => {
      var response = userTypes as ListResponse<userType>;
      this.userTypeListAddForm = this.userTypeListAddForm.concat(response.model);
      this.userTypeListEditForm = this.userTypeListEditForm.concat(response.model);

      var selectedData: userType = {
        id: -1,
        typeName: "Select a user type"
      };

      this.userTypeListAddForm.unshift(selectedData);
      this.userTypeListEditForm.unshift(selectedData);

      this.addAdminUserModel.userTypeId = -1;
    })
  }

  saveAdminUser(){
    this.genericService.controllerName = "AdminUser";
    this.genericService.post<adminUser>(this.addAdminUserModel).subscribe(() => {
      this._toastr.success('Admin user added successfully');
      this.getAllAdminUser();
      this.resetForms("add");
    })
  }

  getAdminUser(id: number){
    this.genericService.controllerName = "AdminUser";
    this.genericService.get(id).subscribe(result => {
      var response = result as SingleResponse<adminUser>;
      this.editAdminUserModel = response.model;
    })
  }

  updateAdminUser(id: number, adminUser: adminUser){
    this.genericService.put<adminUser>(id, adminUser).subscribe(() => {
      this._toastr.success('Admin user updated successfully');
      this.adminUserEditFormCloseButton.nativeElement.click();
      this.resetForms("edit");
      this.getAllAdminUser();
    })
  }

  deleteAdminUser(id: number){
    this.genericService.delete(id).subscribe(result => {
      this._toastr.warning('Admin user deleted successfully');
      this.getAllAdminUser();
    })
  }

  resetForms(formType: string){
    if(formType == "add")
    {
      this.adminUserAddForm.reset({
        userName: "",
        userPassword: "",
        userEmail: "",
        userTypeDDL: -1
      });
    }
    else if(formType == "edit")
    {
      this.adminUserEditForm.reset({
        editUserName: "",
        editUserPassword: "",
        editUserEmail: "",
        editUserType: -1
      });
    }
  }
}
