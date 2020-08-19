import { Component, OnDestroy, OnInit } from '@angular/core';
import { OidcClientNotification, OidcSecurityService, PublicConfiguration } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})


export class AppComponent implements OnInit {
  isAuthenticated: boolean;
  constructor(public oidcSecurityService: OidcSecurityService, private http: HttpClient) {}
  
  ngOnInit() {
    this.oidcSecurityService.checkAuth().subscribe((auth) => {
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
  
  login() {
    this.oidcSecurityService.authorize();
  }
  
  logout() {
      console.log("Logout");
      this.oidcSecurityService.logoff();
      localStorage.removeItem("ACCESS_TOKEN");
  }
  
  callApi(){
    const token = this.oidcSecurityService.getToken();
      this.http.get("http://localhost:52717/api/products")
      .subscribe((data: any) => console.log(data));
  }
}
