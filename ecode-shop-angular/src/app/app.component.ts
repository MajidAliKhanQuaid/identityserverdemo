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
  constructor(public oidcSecurityService: OidcSecurityService, private http: HttpClient) {}
  
  ngOnInit() {
    this.oidcSecurityService.checkAuth().subscribe((auth) => console.log('is authenticated', auth));
  }
  
  login() {
    this.oidcSecurityService.authorize();
  }
  
  logout() {
    this.oidcSecurityService.logoff();
  }
  
  callApi(){
    const token = this.oidcSecurityService.getToken();
    console.log("Calling the Api : ", token);
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token,
      }),
    };
    this.http.get("http://localhost:52717/WeatherForecast/list", httpOptions)
    .subscribe((data: any) => console.log(data));
  }
}
