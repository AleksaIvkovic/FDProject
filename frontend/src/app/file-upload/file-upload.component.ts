import { Component, OnInit } from '@angular/core';
import { NgxDropzoneChangeEvent } from 'ngx-dropzone';
import { forkJoin } from 'rxjs';
import { Configuration } from '../models/configuration.model';
import { ConfigurationService } from '../services/configuration.service';
import { FileService } from '../services/file-service.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  files: File[] = [];
  configuration: Configuration | undefined = undefined

  constructor(private fileService: FileService, private configurationService: ConfigurationService) { }

  ngOnInit(): void {
    this.configurationService.getConfiguration().subscribe(
      data => {
        this.configuration = data;
      },
      error => {
        alert('There was a server error, please try again by reloading the page');
      }
    )
  }

  onSelect(event: NgxDropzoneChangeEvent) {
    console.log(event);
    this.files.push(...event.addedFiles);
  }
  
  onRemove(file: File) {
    console.log(file);
    this.files.splice(this.files.indexOf(file), 1);
  }

  onUpload() {
    this.fileService.uploadFiles(this.files)?.subscribe(
      message => {
      },
      response => {
        if(response.status === 200)
        {
          this.files = [];
          alert('files were successfully uploaded to the server');
          this.fileService.onFilesChange();
        }
        else{
          alert("There was a server error. Please try again");
        }
      }
    )
  }
}
