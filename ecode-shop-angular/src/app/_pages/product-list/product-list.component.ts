import { ProductService } from './../../_services/product.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { API_BASE_URL } from './../../../environments/environment';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
})
export class ProductListComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  imageHostingPath: string;
  products: any[];
  displayedColumns: string[] = [
    'image',
    'code',
    'name',
    'description',
    'price',
    'edit',
  ];
  dataSource = new MatTableDataSource();

  constructor(private productService: ProductService) {
    this.imageHostingPath = API_BASE_URL;
  }

  ngOnInit(): void {
    this.productService.getProducts().subscribe((x) => {
      console.log('Product List ', x);
      this.products = x;
      this.dataSource.paginator = this.paginator;
      this.dataSource.data = this.products;
    });
  }

  editProduct(pId) {
    console.log('Product Id ', pId);
  }
}
