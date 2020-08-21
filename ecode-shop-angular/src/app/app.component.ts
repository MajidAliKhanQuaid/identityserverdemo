import { ProductService } from './_services/product.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { OidcClientNotification, OidcSecurityService, PublicConfiguration } from 'angular-auth-oidc-client';
import { Observable, Subscription } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})


export class AppComponent implements OnInit, OnDestroy {
  product: any;
  subscription1: Subscription;
  subscription2: Subscription;
  isAuthenticated: boolean;
  products: any[];
  constructor(public oidcSecurityService: OidcSecurityService, private productService: ProductService) {}
  
  ngOnInit() {
    this.product = {imageUrl : 'https://via.placeholder.com/150', title: 'first image'}
    this.subscription1 = this.oidcSecurityService.checkAuth().subscribe((auth) => {
      if(!auth){
        this.isAuthenticated = false;
        console.warn("ngOnInit | User Authenticated ", auth);
        this.login();
      }
      else{
        this.isAuthenticated = true;
        //
        const token = this.oidcSecurityService.getToken();
        console.warn("login | Token || ", token);
        const refreshToken = this.oidcSecurityService.getRefreshToken();
        console.warn("login | Refresh Token || ", refreshToken);
      }
    });
  }


  ngOnDestroy(){
    this.subscription1.unsubscribe();
    this.subscription2.unsubscribe();
  }
  
  login() {
    this.oidcSecurityService.authorize();
  }
  
  logout() {
      console.log("Logout");
      this.oidcSecurityService.logoff();
      localStorage.removeItem("ACCESS_TOKEN");
  }
  
  callApi(){
    this.subscription2= this.productService.getProducts().subscribe(products => {
      this.products = products;
      console.log(this.products);
    })
  }
}
