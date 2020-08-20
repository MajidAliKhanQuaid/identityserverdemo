import { ProductService } from './../../_services/product.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { API_BASE_URL } from './../../../../../environments/environment';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  imageHostingPath: string;
  products : any[];
  constructor(private productService : ProductService) {
    this.imageHostingPath = API_BASE_URL;
   }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(x => {
      console.log("Product List ", x);
      this.products = x;
    })
  }

}
