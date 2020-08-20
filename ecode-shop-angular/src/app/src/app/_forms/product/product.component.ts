import { ProductService } from './../../_services/product.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

// multiple image upload
// https://hdtuto.com/article/angular-multiple-image-upload-example-multiple-image-upload-in-angular-9

export function uuidv4() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}

export function requiredFileType( type: string ) {
  return function (control: FormControl) {
    const file = control.value;
    if ( file ) {
      const extension = file.name.split('.')[1].toLowerCase();
      if ( type.toLowerCase() !== extension.toLowerCase() ) {
        return {
          requiredFileType: true
        };
      }
      
      return null;
    }

    return null;
  };
}

// export class ProductImage{
//   id: string;
//   image: ;
// }

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})

export class ProductComponent implements OnInit {
  tempImages = []
  productForm: FormGroup;
  productImages: File[];
  errorMessage: string;

  constructor(private productService : ProductService, private router: Router) { }

  ngOnInit(): void {
    this.productForm = new FormGroup({
      ProductCode: new FormControl('PR-', Validators.required),
      ProductName: new FormControl('', Validators.required),
      Description: new FormControl('', Validators.required),
      Price: new FormControl('', Validators.required),
      Stock: new FormControl('', Validators.required),
      Image: new FormControl('')
    });
  }

  submitForm(){
    // console.log("Image Before Submission ", this.productForm.controls.Image.value);
    // console.log('Product Form Validation ', this.productForm);
    console.log(this.productForm.controls.ProductCode.errors);
    if(this.productForm.valid){
      console.log("Form Values : ", this.productForm.value);
      this.productService.saveProduct(this.productForm.value).subscribe((result) => {
        // this.errorMessage = "Request could not be processed, please try again";
        console.log("Product Save | SUCCESS");
        console.log(result);
        this.router.navigate(['/products']);
      }, (error) => {
        // this.errorMessage = "Request could not be processed, please try again";
        console.log("Product Save | FAILURE");
        console.log(error);
      });
    }
    else{
      this.productForm.markAllAsTouched();
    }
  }

  get ProductName(){
    return this.productForm.controls.ProductName;
  }

  get ProductCode(){
    return this.productForm.controls.ProductCode;
  }

  get Description(){
    return this.productForm.controls.Description;
  }

  get Price(){
    return this.productForm.controls.Price;
  }

  get Stock(){
    return this.productForm.controls.Stock;
  }

  imageUploadChange(_event){
    this.productImages = <File[]>_event.target.files;
    for(var i = 0; i < this.productImages.length; i++){
      var reader = new FileReader();
      reader.onload = (_rEvent) => {
        console.log("Read to Add image", _rEvent.target.result); 
        this.tempImages.push(_rEvent.target.result); 
        console.log("Image Added", this.tempImages.length);
        this.productForm.patchValue({
          Image: this.productImages[0]
        })
      };
      reader.readAsDataURL(this.productImages[i]); 
    }
  }

}
