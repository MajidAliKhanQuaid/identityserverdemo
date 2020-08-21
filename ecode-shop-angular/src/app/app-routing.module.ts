import { TagComponent } from './_forms/tag/tag.component';
import { ProductComponent } from './_forms/product/product.component';
import { SalesListComponent } from './_pages/sales-list/sales-list.component';
import { ImagesListComponent } from './_pages/images-list/images-list.component';
import { ProductListComponent } from './_pages/product-list/product-list.component';
import { TagsListComponent } from './_pages/tags-list/tags-list.component';
import { DefaultComponent } from './_components/default/default.component';
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
