import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  getSubCategoriesByCategoryId(id: number){
    return this.http.get(environment.apiUrl + "ProductSubCategory/GetSubCategoryByCategory/" + id);
  }
}
