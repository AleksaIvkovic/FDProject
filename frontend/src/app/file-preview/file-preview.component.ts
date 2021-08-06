import { Component, OnInit } from '@angular/core';
import { FileGroup } from '../models/fileGroup.model';
import { FileService } from '../services/file-service.service';

@Component({
  selector: 'app-file-preview',
  templateUrl: './file-preview.component.html',
  styleUrls: ['./file-preview.component.css']
})
export class FilePreviewComponent implements OnInit {

  files:FileGroup[] = []

  constructor(private fileService: FileService) { }

  ngOnInit(): void {
    this.fileService.getAllFiles().subscribe(
      data => {
        this.files = data;
      },
      error => {
        alert('Files couldn\'t be fethced from the server');
      }
    );

    this.fileService.filesChanged.subscribe(
      change => {
        this.fileService.getAllFiles().subscribe(
          data => {
            this.files = data;
          },
          error => {
            alert('Files couldn\'t be fethced from the server');
          }
        );
      }
    )
  }

}
