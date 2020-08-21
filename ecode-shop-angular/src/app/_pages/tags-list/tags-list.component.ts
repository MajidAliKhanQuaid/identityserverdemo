import { TagService } from './../../_services/tag.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-tags-list',
  templateUrl: './tags-list.component.html',
  styleUrls: ['./tags-list.component.scss'],
})
export class TagsListComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  tags: any[];
  displayedColumns: string[] = ['name', 'description', 'edit'];
  dataSource = new MatTableDataSource();
  constructor(private tagService: TagService) {}

  ngOnInit(): void {
    this.tagService.getTags().subscribe((x) => {
      this.tags = x;
      this.dataSource.paginator = this.paginator;
      this.dataSource.data = this.tags;
    });
  }
}
