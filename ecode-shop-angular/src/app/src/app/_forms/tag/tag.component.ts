import { TagService } from './../../_services/tag.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.scss']
})
export class TagComponent implements OnInit {
  tagForm : FormGroup;
  constructor(private tagService : TagService, private router : Router) { }

  ngOnInit(): void {
    this.tagForm = new FormGroup({
      TagName: new FormControl('', Validators.required),
      TagDescription: new FormControl('', Validators.required)
    });
  }

  saveTag(){
    this.tagService.saveTag(this.tagForm.value).subscribe((result) => {
      this.router.navigate(['/tags']);
    }, (error) => {
      console.log("Product Save | FAILURE");
      console.log(error);
    });
  }

  get TagName(){
    return this.tagForm.controls.TagName;
  }

  get TagDescription(){
    return this.tagForm.controls.TagDescription;
  }

}
