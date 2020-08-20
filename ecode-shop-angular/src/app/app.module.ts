import { ProductComponent } from './src/app/_forms/product/product.component';
import { TokenInterceptor } from './src/app/_interceptors/token.interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule} from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FlexLayoutModule } from '@angular/flex-layout';

import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';
import { ProductBoxComponent } from './src/app/_components/product-box/product-box.component';
import { TopbarComponent } from './src/app/_components/topbar/topbar.component';
import { DefaultComponent } from './src/app/_components/default/default.component';
import { ProductListComponent } from './src/app/_pages/product-list/product-list.component';
import { TagsListComponent } from './src/app/_pages/tags-list/tags-list.component';
import { ImagesListComponent } from './src/app/_pages/images-list/images-list.component';
import { SalesListComponent } from './src/app/_pages/sales-list/sales-list.component';
import { TagComponent } from './src/app/_forms/tag/tag.component';

export function configureAuth(oidcConfigService: OidcConfigService) {
  return () =>
      oidcConfigService.withConfig({
          stsServer: 'http://localhost:5000',
          redirectUrl: window.location.origin,
          postLogoutRedirectUri: window.location.origin,
          clientId: 'client_angular',
          scope: 'openid profile ApiToBeSecured email offline_access',
          responseType: 'code',
          silentRenew: true,
          useRefreshToken: true,
          logLevel: LogLevel.Error | LogLevel.Warn,
      });
}

@NgModule({
  declarations: [AppComponent, ProductBoxComponent, ProductComponent, TopbarComponent, DefaultComponent, ProductListComponent, TagsListComponent, ImagesListComponent, SalesListComponent, TagComponent],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    AuthModule.forRoot()
  ],
  providers: [
    OidcConfigService,
    {
        provide: APP_INITIALIZER,
        useFactory: configureAuth,
        deps: [OidcConfigService],
        multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
