import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { throwError, Observable } from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import { API_URL } from './../../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ProductService{

  constructor(private http: HttpClient) { }

  getProducts = () : Observable<any[]> => {
    return this.http.get(`${API_URL}/products`)
    .pipe(map(x => <[]>x), catchError(err => []))
  }

  saveProduct(formValues)  {
    console.log("Form Values are ", formValues, " Product Name  ", formValues.ProductName);
    const formData = new FormData();
    formData.append("ProductCode", formValues.ProductCode);
    formData.append("ProductName", formValues.ProductName);
    formData.append("Description", formValues.Description);
    formData.append("Price", formValues.Price);
    formData.append("Stock", formValues.Stock);
    formData.append("Image", formValues.Image);
    console.log("FormData is : ", formData);
    //
    return this.http.post(`${API_URL}/products`, formData);
  }

}
