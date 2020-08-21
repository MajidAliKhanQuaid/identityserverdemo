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
  };

  getProductById = (_id): Observable<any> => {
    return this.http.get(`${API_URL}/products/${_id}`)
    .pipe(map(x => x), catchError(err => null))
  };

  saveProduct(_formValues)  {
    console.log("Form Values are ", _formValues, " Product Name  ", _formValues.ProductName);
    const formData = new FormData();
    formData.append("Id", _formValues.Id);
    formData.append("ProductCode", _formValues.ProductCode);
    formData.append("ProductName", _formValues.ProductName);
    formData.append("Description", _formValues.Description);
    formData.append("Price", _formValues.Price);
    formData.append("Stock", _formValues.Stock);
    formData.append("Image", _formValues.Image);
    console.log("FormData is : ", formData);
    //
    return this.http.post(`${API_URL}/products`, formData);
  }

}
