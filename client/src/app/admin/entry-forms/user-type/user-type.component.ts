import { Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from "@angular/common";
import { GenericService } from '../../../_services/generic.service';
import { userType } from '../../../_models/userType';
import { ListResponse } from '../../../_responses/listResponse';
import { FormsModule, NgForm } from '@angular/forms';
import { SingleResponse } from '../../../_responses/singleResponse';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-type',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-type.component.html',
  styleUrl: './user-type.component.css'
})
export class UserTypeComponent {
  @ViewChild("userTypeAddForm") userTypeAddForm!: NgForm;
  @ViewChild("userTypeEditForm") userTypeEditForm!: NgForm;

  userTypeList: userType[] = [];
  addUserType: userType = {
    id: 0,
    typeName: ""
  };

  editUserType: userType = {
    id: 0,
    typeName: ""
  };
  
  //isAddFormSubmitted: boolean = false;

  @ViewChild("userTypeEditFormCloseButton") userTypeEditFormCloseButton!: ElementRef;

  constructor(private genericService: GenericService, private _toastr: ToastrService){

  }

  ngOnInit(){
    this.genericService.controllerName = "UserType";
    this.getAllUserTypes();
  }

  getAllUserTypes(){
    this.genericService.getAll().subscribe(userTypes => {
      var response = userTypes as ListResponse<userType>;
      this.userTypeList = response.model;
    })
  }

  saveUserType(){
    this.genericService.post<userType>(this.addUserType).subscribe(() => {
      this._toastr.success('User type added successfully');
      this.resetForms("add");
      this.getAllUserTypes();
    })
  }

  getUserType(id: number){
    this.genericService.get(id).subscribe(result => {
      var response = result as SingleResponse<userType>;
      this.editUserType = response.model;
    })
  }

  updateUserType(id: number, userType: userType){
    this.genericService.put<userType>(id, userType).subscribe(() => {
      this.userTypeEditFormCloseButton.nativeElement.click();
      this._toastr.success('User type updated successfully');
      this.resetForms("edit");
      this.getAllUserTypes();
    })
  }

  deleteUserType(id: number){
    this.genericService.delete(id).subscribe(result => {
      this._toastr.warning('User type deleted successfully');
      this.getAllUserTypes();
    })
  }

  resetForms(formType: string){
    if(formType == "add")
    {
      this.userTypeAddForm.reset({
        typeName: ""
      });
    }
    else if(formType == "edit"){
      this.userTypeEditForm.reset({
        editTypeName: ""
      });
    }
  }
}
