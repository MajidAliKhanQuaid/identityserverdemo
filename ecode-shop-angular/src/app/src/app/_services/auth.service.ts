import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Injectable } from '@angular/core';
import * as jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private oidcSecurityService: OidcSecurityService) { }

  login() {
    this.oidcSecurityService.authorize();
  }
  
  logout() {
      this.oidcSecurityService.logoff();
  }

  getToken(){
    return this.oidcSecurityService.getToken();
  }

  getRefreshToken(){
    return this.oidcSecurityService.getRefreshToken();
  }

  getUserData(){
    const token = this.getToken();
    try{
      return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
  }

}
