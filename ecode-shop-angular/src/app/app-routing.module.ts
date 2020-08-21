import { TagComponent } from './src/app/_forms/tag/tag.component';
import { ProductComponent } from './src/app/_forms/product/product.component';
import { SalesListComponent } from './src/app/_pages/sales-list/sales-list.component';
import { ImagesListComponent } from './src/app/_pages/images-list/images-list.component';
import { ProductListComponent } from './src/app/_pages/product-list/product-list.component';
import { TagsListComponent } from './src/app/_pages/tags-list/tags-list.component';
import { DefaultComponent } from './src/app/_components/default/default.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';

const routes: Routes = [
  {path: '', component: DefaultComponent},
  {path: 'add-product', component: ProductComponent},
  {path: 'edit-product/:id', component: ProductComponent},
  {path: 'products', component: ProductListComponent},
  {path: 'add-tag', component: TagComponent},
  {path: 'edit-tag/:id', component: TagComponent},
  {path: 'tags', component: TagsListComponent},
  {path: 'images', component: ImagesListComponent},
  {path: 'sales', component: SalesListComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
