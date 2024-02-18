import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from "@angular/forms";
import { adminUser } from '../../../_models/adminUser';
import { GenericService } from '../../../_services/generic.service';
import { ListResponse } from '../../../_responses/listResponse';
import { CommonModule } from '@angular/common';
import { userType } from '../../../_models/userType';
import { SingleResponse } from '../../../_responses/singleResponse';

@Component({
  selector: 'app-admin-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-user.component.html',
  styleUrl: './admin-user.component.css'
})
export class AdminUserComponent {
  @ViewChild("adminUserAddForm") adminUserAddForm!: NgForm;
  @ViewChild("adminUserEditForm") adminUserEditForm!: NgForm;

  adminUserList: adminUser[] = [];
  userTypeListAddForm: userType[] = [];
  userTypeListEditForm: userType[] = [];
  addAdminUser: adminUser = new adminUser();
  editAdminUser: adminUser = new adminUser();

  @ViewChild("adminUserEditFormCloseButton") adminUserEditFormCloseButton!: ElementRef;

  constructor(private genericService: GenericService){

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
      this.userTypeListAddForm = response.model;
      this.userTypeListEditForm = response.model;

      var selectedData: userType = {
        id: -1,
        typeName: "Select a user type"
      };

      this.userTypeListAddForm.unshift(selectedData);
      this.userTypeListEditForm.unshift(selectedData);
      console.log(response.model);
      console.log(this.userTypeListEditForm);
      console.log(this.userTypeListEditForm);

      this.addAdminUser.userTypeId = -1;
    })
  }

  saveAdminUser(){
    this.genericService.controllerName = "AdminUser";
    this.genericService.post<adminUser>(this.addAdminUser).subscribe(() => {
      this.getAllAdminUser();
      this.resetForms("add");
    })
  }

  getAdminUser(id: number){
    this.genericService.controllerName = "AdminUser";
    this.genericService.get(id).subscribe(result => {
      var response = result as SingleResponse<adminUser>;
      this.editAdminUser = response.model;
    })
  }

  updateAdminUser(id: number, adminUser: adminUser){
    this.genericService.put<adminUser>(id, adminUser).subscribe(() => {
      this.adminUserEditFormCloseButton.nativeElement.click();
      this.resetForms("edit");
      this.getAllAdminUser();
    })
  }

  deleteAdminUser(id: number){
    this.genericService.delete(id).subscribe(result => {
      this.getAllAdminUser();
    })
  }

  resetForms(formType: string){
    if(formType == "add")
    {
      this.adminUserAddForm.reset();
    }
    else if(formType == "edit")
    {
      this.adminUserEditForm.reset();
    }
  }
}
