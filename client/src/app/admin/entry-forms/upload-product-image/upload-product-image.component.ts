import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { uploadProductImage } from '../../../_models/uploadProductImage';
import { GenericService } from '../../../_services/generic.service';
import { ListResponse } from '../../../_responses/listResponse';
import { product } from '../../../_models/product';
import { CommonModule } from '@angular/common';
import { FileUploadService } from '../../../_services/file-upload.service';
import { SingleResponse } from '../../../_responses/singleResponse';
import { ToastrService } from 'ngx-toastr';
import { productImage } from '../../../_models/productImage';

@Component({
  selector: 'app-upload-product-image',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './upload-product-image.component.html',
  styleUrl: './upload-product-image.component.css'
})
export class UploadProductImageComponent {
  fileName: string = "";
  productImageList: productImage[] = [];
  productListAddForm: product[] = [];

  uploadProductImageAddFormModel: uploadProductImage = {
    file: new File([""], "", {type: "text/plain", lastModified: undefined}),
    isMain: false,
    productId: 0
  }

  constructor(
    private genericService: GenericService,
    private fileUploadService: FileUploadService,
    private _toastr: ToastrService){}

  ngOnInit(){
    this.getAllProducts();
    this.getAllProductImages();
  }

  @ViewChild("productImageUploadForm") productImageUploadForm!: NgForm;

  getAllProducts(){
    this.genericService.controllerName = "Product";
    this.genericService.getAll().subscribe(products => {
      var response = products as ListResponse<product>;
      this.productListAddForm = this.productListAddForm.concat(response.model);

      var selectedData: product = {
        id: -1,
        name: "Select a product",
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

      this.productListAddForm.unshift(selectedData);

      this.uploadProductImageAddFormModel.productId = -1;
    })
  }

  getAllProductImages(){
    this.genericService.controllerName = "ProductImage";
    this.genericService.getAll().subscribe(productImages => {
      var response = productImages as ListResponse<productImage>;
      this.productImageList = response.model;
    })
  }

  onFileSelectedForProductImage(event: Event) {

    const element = event.target as HTMLInputElement;
    if(element.files != null){
      const file: File = element.files[0];

      if (file) {
        this.fileName = file.name;
        this.uploadProductImageAddFormModel.file = file;
      }
    }
  }

  uploadAndSaveProductImage(){
    const formData = new FormData();
    formData.append("File", this.uploadProductImageAddFormModel.file);
    formData.append("ProductId", this.uploadProductImageAddFormModel.productId.toString());
    formData.append("IsMain", String(this.uploadProductImageAddFormModel.isMain));

    this.fileUploadService.uploadProductImage(formData).subscribe(() => {
      this._toastr.success('Product image uploaded successfully');
      this.resetForms("add");
    })
  }

  deleteProductImage(id: number){
    this.genericService.delete(id).subscribe(result => {
      this._toastr.warning('Product image deleted successfully');
      this.getAllProductImages();
    })
  }

  resetForms(formType: string){
    if(formType == "add")
    {
      this.productImageUploadForm.reset({
        productImageFileInput: "",
        productNameDDL: -1,
        isMainImage: false
      });
    }

    this.uploadProductImageAddFormModel = {
      file: new File([""], "", {type: "text/plain", lastModified: undefined}),
      isMain: false,
      productId: 0
    };
    // else if(formType == "edit"){
    //   this.userTypeEditForm.reset({
    //     editTypeName: ""
    //   });
    // }
  }
}
