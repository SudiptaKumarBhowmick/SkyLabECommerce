import { Component, ElementRef, ViewChild } from '@angular/core';
import { productCategory } from '../../../_models/productCategory';
import { productSubCategory } from '../../../_models/productSubCategory';
import { product } from '../../../_models/product';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GenericService } from '../../../_services/generic.service';
import { ListResponse } from '../../../_responses/listResponse';
import { ValidateSelectOptionDirective } from '../../../_directives/validate-select-option.directive';
import { SingleResponse } from '../../../_responses/singleResponse';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    ValidateSelectOptionDirective
  ],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  @ViewChild("productAddForm") productAddForm!: NgForm;
  @ViewChild("productEditForm") productEditForm!: NgForm;

  productCategoryListAddForm: productCategory[] = [];
  productSubCategoryListAddForm: productSubCategory[] = [];

  productCategoryListEditForm: productCategory[] = [];
  productSubCategoryListEditForm: productSubCategory[] = [];

  productList: product[] = [];

  addProductFormsModel: product = {
    id: 0,
    name: "",
    description: "",
    productCategoryId: -1,
    productSubCategoryId: -1,
    productCategory: {
      id: 0,
      categoryName: ""
    },
    productSubCategory: {
      id: 0,
      subCategoryName: "",
      productCategoryId: 0,
      productCategory: {
        id: 0,
        categoryName: ""
      }
    }
  };

  editProductFormsModel: product = {
    id: 0,
    name: "",
    description: "",
    productCategoryId: -1,
    productSubCategoryId: -1,
    productCategory: {
      id: 0,
      categoryName: ""
    },
    productSubCategory: {
      id: 0,
      subCategoryName: "",
      productCategoryId: 0,
      productCategory: {
        id: 0,
        categoryName: ""
      }
    }
  };

  @ViewChild("productEditFormCloseButton") productEditFormCloseButton!: ElementRef;

  constructor(private genericService: GenericService){

  }

  ngOnInit(){
    this.getAllProductCategories();
    this.getAllProductSubCategories();
    this.getAllProducts();
  }

  getAllProducts(){
    this.genericService.controllerName = "Product";
    this.genericService.getAll().subscribe(products => {
      var response = products as ListResponse<product>;
      this.productList = response.model;
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

      this.addProductFormsModel.productCategoryId = -1;
    })
  }

  getAllProductSubCategories(){
    this.genericService.controllerName = "ProductSubCategory";
    this.genericService.getAll().subscribe(subCategories => {
      var response = subCategories as ListResponse<productSubCategory>;
      this.productSubCategoryListAddForm = this.productSubCategoryListAddForm.concat(response.model);
      this.productSubCategoryListEditForm = this.productSubCategoryListEditForm.concat(response.model);

      var selectedData: productSubCategory = {
        id: -1,
        subCategoryName: "Select a sub category",
        productCategoryId: 0,
        productCategory: {
          id: 0,
          categoryName: ""
        }
      };

      this.productSubCategoryListAddForm.unshift(selectedData);
      this.productSubCategoryListEditForm.unshift(selectedData);

      this.addProductFormsModel.productSubCategoryId = -1;
    })
  }

  saveProductSubCategory(){
    this.genericService.controllerName = "Product";
    this.genericService.post(this.addProductFormsModel).subscribe(() => {

    })
  }

  getProduct(id: number){
    this.genericService.controllerName = "Product";
    this.genericService.get(id).subscribe(result => {
      var response = result as SingleResponse<product>;
      this.editProductFormsModel = response.model;
    })
  }

  updateProductSubCategory(id: number, product: product){
    this.genericService.controllerName = "Product";
    this.genericService.put<product>(id, product).subscribe(() => {
      this.productEditFormCloseButton.nativeElement.click();
      this.resetForms("edit");
      this.getAllProducts();
    })
  }

  deleteProduct(id: number){
    this.genericService.controllerName = "Product";
    this.genericService.delete(id).subscribe(result => {
      this.getAllProducts();
    })
  }

  resetForms(formType: string){
    if(formType == "add")
    {
      this.productAddForm.reset({
        productName: "",
        productDescription: "",
        productCategoryDDL: -1,
        productSubCategoryDDL: -1
      });
    }
    else if(formType == "edit")
    {
      this.productEditForm.reset({
        productName: "",
        productDescription: "",
        productCategoryDDL: -1,
        productSubCategoryDDL: -1
      });
    }
  }
}
