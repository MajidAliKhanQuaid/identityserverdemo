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
        Authorization: 'Bearer ' + "eyJhbGciOiJSUzI1NiIsImtpZCI6IjAzMUU2NjgwMkI4MjZDQkQ1QUUzMTY3QjhBMTFDQzE0IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE1OTczMzg3NTQsImV4cCI6MTU5NzM0MjM1NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiQXBpVG9CZVNlY3VyZWQiLCJjbGllbnRfaWQiOiJjbGllbnRfYW5ndWxhciIsInN1YiI6IjRmNGQwYzAzLTUzOGMtNGZjZS1hYTBhLTE3NmYzMGNkYmM5ZCIsImF1dGhfdGltZSI6MTU5NzMzODc1MywiaWRwIjoibG9jYWwiLCJqdGkiOiJEMEY5NTVCQzg3NUU3MUI1QjEzMzVFQjFBM0MyMjA0MyIsInNpZCI6IkJENEFDREY2RkREMEVFODM5ODYzQjM3NzlDRTcwOEE3IiwiaWF0IjoxNTk3MzM4NzU0LCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiQXBpVG9CZVNlY3VyZWQiLCJlbWFpbCJdLCJhbXIiOlsicHdkIl19.bHy4iXFnWsNSoGBp9DVD_wViwbiwcVvbjFHgRq1IxOmLmQJY466CUJ6MjsRvXpBDEM5zZlsPjCkg6WViIyDAdEINE5f_xD1OhjLeq8mwXBku7SWEEYIrtNZQ8Qa67_SetuWGQw0XPaYxBze2UOtvyxZrQDlAy32gnwE_avSzPP-ppM6clI4m2PYGgg3F5TKZ4JN3SPoyiHfZjhsZILr8Qy7jD26u4MFpcTVqCk2L3rpzKgSLJ32PI06Klc9p4UNrJ584NEPEklYKCgTbeSJuz5owNzlI6Ahvw8vgHV6sFy-KQ04yQ3yvnlVkvcfhAvft81vqTwC62-39QrUUSr7HVg",
      }),
    };
    this.http.get("http://localhost:52717/WeatherForecast/list", httpOptions)
    .subscribe((data: any) => console.log(data));
  }
}
