import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { uploadProductImage } from '../../../_models/uploadProductImage';
import { GenericService } from '../../../_services/generic.service';
import { ListResponse } from '../../../_responses/listResponse';
import { product } from '../../../_models/product';
import { CommonModule } from '@angular/common';

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
  productListAddForm: product[] = [];

  uploadProductImageAddFormModel: uploadProductImage = {
    file: new File([""], "", {type: "text/plain", lastModified: undefined}),
    isMain: false,
    productId: 0
  }

  constructor(private genericService: GenericService){}

  ngOnInit(){
    this.getAllProducts();
  }

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

  onFileSelectedForProductImage(event: Event) {

    const element = event.target as HTMLInputElement;
    if(element.files != null){
      const file: File = element.files[0];

      if (file) {

        this.fileName = file.name;

        console.log(file);

        this.uploadProductImageAddFormModel.file = file;

        console.log(this.uploadProductImageAddFormModel);
  
        //const formData = new FormData();
  
        //formData.append("thumbnail", file);
  
        //const upload$ = this.http.post("/api/thumbnail-upload", formData);
  
        //upload$.subscribe();

        //https://blog.angular-university.io/angular-file-upload/
      }
    }
  }

  uploadAndSaveProductImage(){

  }
}
