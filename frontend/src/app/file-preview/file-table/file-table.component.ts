import { Component, Input, OnInit } from '@angular/core';
import { FileGroup } from 'src/app/models/fileGroup.model';

@Component({
  selector: 'app-file-table',
  templateUrl: './file-table.component.html',
  styleUrls: ['./file-table.component.css']
})
export class FileTableComponent implements OnInit {
  @Input() fileArray: FileGroup | undefined = undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
