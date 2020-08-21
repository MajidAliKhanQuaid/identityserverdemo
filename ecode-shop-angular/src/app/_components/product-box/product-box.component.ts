import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-box',
  templateUrl: './product-box.component.html',
  styleUrls: ['./product-box.component.scss']
})
export class ProductBoxComponent implements OnInit {
  @Input() product: any;
  constructor() { }

  ngOnInit(): void {
  }

  addToCart(event){
    console.log("Event : ", event);
  }

  edit(event){
    console.log("Edit ", event);
  }

}
