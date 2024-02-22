import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { productSubCategory } from '../../../_models/productSubCategory';
import { CommonModule } from '@angular/common';
import { GenericService } from '../../../_services/generic.service';
import { productCategory } from '../../../_models/productCategory';
import { ListResponse } from '../../../_responses/listResponse';
import { ValidateSelectOptionDirective } from '../../../_directives/validate-select-option.directive';
import { SingleResponse } from '../../../_responses/singleResponse';

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
  @ViewChild("productSubCategoryEditForm") productSubCategoryEditForm!: NgForm;

  productSubCategoryList: productSubCategory[] = [];
  productCategoryListAddForm: productCategory[] = [];
  productCategoryListEditForm: productCategory[] = [];

  addProductSubCategoryFormsModel: productSubCategory = {
    id: 0,
    subCategoryName: "",
    productCategoryId: -1,
    productCategory: {
      id: 0,
      categoryName: ""
    }
  };

  editProductSubCategoryFormsModel: productSubCategory = {
    id: 0,
    subCategoryName: "",
    productCategoryId: -1,
    productCategory: {
      id: 0,
      categoryName: ""
    }
  };

  @ViewChild("productSubCategoryEditFormCloseButton") productSubCategoryEditFormCloseButton!: ElementRef;
  
  constructor(private genericService: GenericService){

  }

  ngOnInit(){
    this.getAllProductCategories();
    this.getAllProductSubCategories();
  }

  getAllProductSubCategories(){
    this.genericService.controllerName = "ProductSubCategory";
    this.genericService.getAll().subscribe(subCategories => {
      var response = subCategories as ListResponse<productSubCategory>;
      this.productSubCategoryList = response.model;
    })
  }

  getAllProductCategories(){
    this.genericService.controllerName = "ProductCategory";
    this.genericService.getAll().subscribe(categories => {
      var response = categories as ListResponse<productCategory>;
      this.productCategoryListAddForm = this.productCategoryListAddForm.concat(response.model);
      this.productCategoryListEditForm = this.productCategoryListEditForm.concat(response.model);

      var selectedData: productCategory = {
        id: -1,
        categoryName: "Select a category"
      };

      this.productCategoryListAddForm.unshift(selectedData);
      this.productCategoryListEditForm.unshift(selectedData);

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
    this.genericService.controllerName = "ProductSubCategory";
    this.genericService.get(id).subscribe(result => {
      var response = result as SingleResponse<productSubCategory>;
      this.editProductSubCategoryFormsModel = response.model;
    })
  }

  updateProductSubCategory(id: number, productSubCategory: productSubCategory){
    this.genericService.controllerName = "ProductSubCategory";
    this.genericService.put<productSubCategory>(id, productSubCategory).subscribe(() => {
      this.productSubCategoryEditFormCloseButton.nativeElement.click();
      this.resetForms("edit");
      this.getAllProductSubCategories();
    })
  }

  deleteProductSubCategory(id: number){
    this.genericService.controllerName = "ProductSubCategory";
    this.genericService.delete(id).subscribe(result => {
      this.getAllProductSubCategories();
    })
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
      this.productSubCategoryEditForm.reset({
        editSubCategoryName: "",
        editCategoryDDL: -1
      });
    }
  }
}
