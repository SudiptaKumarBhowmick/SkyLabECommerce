import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { productSubCategory } from '../../_models/productSubCategory';
import { CommonModule } from '@angular/common';
import { GenericService } from '../../_services/generic.service';
import { productCategory } from '../../_models/productCategory';
import { ListResponse } from '../../_responses/listResponse';
import { ValidateSelectOptionDirective } from '../../_directives/validate-select-option.directive';

@Component({
  selector: 'app-product-subcategory',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    ValidateSelectOptionDirective
  ],
  templateUrl: './product-subcategory.component.html',
  styleUrl: './product-subcategory.component.css'
})
export class ProductSubcategoryComponent {
  @ViewChild("productSubCategoryAddForm") productSubCategoryAddForm!: NgForm;

  productSubCategoryList: productSubCategory[] = [];
  productCategoryListAddForm: productCategory[] = [];

  addProductSubCategoryFormsModel: productSubCategory = {
    id: 0,
    subCategoryName: "",
    productCategoryId: -1,
    productCategory: {
      id: 0,
      categoryName: ""
    }
  };

  constructor(private genericService: GenericService){

  }

  ngOnInit(){
    this.getAllProductCategories();
    this.getAllProductSubCategories();
  }

  getAllProductSubCategories(){
    this.genericService.controllerName = "ProductSubCategory";
    this.genericService.getAll().subscribe(categories => {
      var response = categories as ListResponse<productSubCategory>;
      this.productSubCategoryList = response.model;
    })
  }

  getAllProductCategories(){
    this.genericService.controllerName = "ProductCategory";
    this.genericService.getAll().subscribe(subCategories => {
      var response = subCategories as ListResponse<productCategory>;
      this.productCategoryListAddForm = this.productCategoryListAddForm.concat(response.model);
      //this.userTypeListEditForm = this.userTypeListEditForm.concat(response.model);

      var selectedData: productCategory = {
        id: -1,
        categoryName: "Select a category"
      };

      this.productCategoryListAddForm.unshift(selectedData);
      //this.userTypeListEditForm.unshift(selectedData);

      this.addProductSubCategoryFormsModel.productCategoryId = -1;
    })
  }

  saveProductSubCategory(){
    this.genericService.controllerName = "ProductSubCategory";
    this.genericService.post<productSubCategory>(this.addProductSubCategoryFormsModel).subscribe(() => {
      this.getAllProductSubCategories();
      this.resetForms("add");
    })
  }

  getProductSubCategory(id: number){
    // this.genericService.controllerName = "AdminUser";
    // this.genericService.get(id).subscribe(result => {
    //   var response = result as SingleResponse<adminUser>;
    //   this.editAdminUserModel = response.model;
    // })
  }

  updateProductSubCategory(id: number, productSubCategory: productSubCategory){
    // this.genericService.put<adminUser>(id, adminUser).subscribe(() => {
    //   this.adminUserEditFormCloseButton.nativeElement.click();
    //   this.resetForms("edit");
    //   this.getAllAdminUser();
    // })
  }

  deleteProductSubCategory(id: number){
    // this.genericService.delete(id).subscribe(result => {
    //   this.getAllAdminUser();
    // })
  }

  resetForms(formType: string){
    if(formType == "add")
    {
      this.productSubCategoryAddForm.reset({
        subCategoryName: "",
        categoryDDL: -1
      });
    }
    else if(formType == "edit")
    {
      // this.adminUserEditForm.reset({
      //   editUserName: "",
      //   editUserPassword: "",
      //   editUserEmail: "",
      //   editUserType: -1
      // });
    }
  }
}
