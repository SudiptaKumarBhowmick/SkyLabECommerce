import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class GenericService {
  controllerName: string = "";

  constructor(private http: HttpClient) { }

  get(id : number){
    return this.http.get(environment.apiUrl + this.controllerName + "/" + id);
  }

  getAll(){
    return this.http.get(environment.apiUrl + this.controllerName);
  }

  post<T>(entity : T){
    return this.http.post(environment.apiUrl + this.controllerName, entity);
  }

  put<T>(id: number, entity: T){
    return this.http.put(environment.apiUrl + this.controllerName + "/" + id, entity);
  }

  delete(id: number){
    return this.http.delete(environment.apiUrl + this.controllerName + "/" + id);
  }
}
