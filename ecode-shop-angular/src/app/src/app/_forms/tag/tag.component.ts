import { Subscription } from 'rxjs';
import { TagService } from './../../_services/tag.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.scss']
})
export class TagComponent implements OnInit {
  tagForm : FormGroup;
  RouteSubscription: Subscription;
  tagId;
  
  constructor(private tagService : TagService, private route: ActivatedRoute ,private router : Router) { }

  ngOnInit(): void {
    this.tagForm = new FormGroup({
      Id: new FormControl('', Validators.required),
      TagName: new FormControl('', Validators.required),
      TagDescription: new FormControl('', Validators.required)
    });

    this.RouteSubscription = this.route.paramMap.subscribe(params => {
      if(params.has('id')){
        this.tagId = params.get('id');
        this.tagService.getTagById(this.tagId).subscribe(tag => {
          console.log("***************** ", tag.id);
          this.tagForm.patchValue({
            Id: tag.id,
            TagName: tag.tagName,
            TagDescription: tag.tagDescription
          });
        })
        console.log('Tag Id ', this.tagId);
      }
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
