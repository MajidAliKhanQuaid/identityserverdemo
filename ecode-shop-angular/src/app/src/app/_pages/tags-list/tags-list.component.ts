import { TagService } from './../../_services/tag.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tags-list',
  templateUrl: './tags-list.component.html',
  styleUrls: ['./tags-list.component.scss']
})
export class TagsListComponent implements OnInit {
  tags: any[];
  constructor(private tagService : TagService) { }

  ngOnInit(): void {
    this.tagService.getTags().subscribe(x => this.tags = x);
  }

}
