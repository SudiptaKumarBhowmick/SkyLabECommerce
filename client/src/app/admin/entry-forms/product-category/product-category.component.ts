import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { productCategory } from '../../../_models/productCategory';
import { CommonModule } from '@angular/common';
import { GenericService } from '../../../_services/generic.service';
import { ListResponse } from '../../../_responses/listResponse';
import { SingleResponse } from '../../../_responses/singleResponse';

@Component({
  selector: 'app-product-category',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './product-category.component.html',
  styleUrl: './product-category.component.css'
})
export class ProductCategoryComponent {
  @ViewChild("productCategoryAddForm") productCategoryAddForm!: NgForm;

  productCategoryList: productCategory[] = [];

  addProductCategoryFormsModel: productCategory = {
    id: 0,
    categoryName: ""
  };

  editProductCategoryFormsModel: productCategory = {
    id: 0,
    categoryName: ""
  };

  @ViewChild("productCategoryEditFormCloseButton") productCategoryEditFormCloseButton!: ElementRef;

  constructor(private genericService: GenericService){
    
  }

  ngOnInit(){
    this.genericService.controllerName = "ProductCategory";
    this.getAllProductCategories();
  }

  getAllProductCategories(){
    this.genericService.getAll().subscribe(userTypes => {
      var response = userTypes as ListResponse<productCategory>;
      this.productCategoryList = response.model;
    })
  }

  saveProductCategory(){
    this.genericService.post<productCategory>(this.addProductCategoryFormsModel).subscribe(() => {
      this.resetForms("add");
      this.getAllProductCategories();
    })
  }

  getProductCategory(id: number){
    this.genericService.get(id).subscribe(result => {
      var response = result as SingleResponse<productCategory>;
      this.editProductCategoryFormsModel = response.model;
    })
  }

  productCategoryUpdateModal(id: number, category: productCategory){
    this.genericService.put<productCategory>(id, category).subscribe(() => {
      this.productCategoryEditFormCloseButton.nativeElement.click();
      this.resetForms("edit");
      this.getAllProductCategories();
    })
  }

  deleteProductCategory(id: number){
    this.genericService.delete(id).subscribe(result => {
      this.getAllProductCategories();
    })
  }

  resetForms(formType: string){
    if(formType == "add"){
      this.productCategoryAddForm.reset({
        categoryName: ""
      });
    }
    else if(formType == "edit"){
      //this.userTypeEditForm.reset();
    }
  }
}
