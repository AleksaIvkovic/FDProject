import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FileGroup } from '../models/fileGroup.model';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  filesChanged = new Subject<boolean>();
  private state = false;

  constructor(private httpClient: HttpClient) { }

  onFilesChange(){
    this.filesChanged.next(this.state);
    this.state = !this.state
  }

  getAllFiles(){
    return this.httpClient.get<FileGroup[]>(environment.fileController);
  }

  uploadFiles(files: File[]) {
    if (files.length === 0) {
      return;
    }
    const formData = new FormData();
      
    Array.from(files).map((file, index) => {
      return formData.append('file'+index, file, file.name);
    });

    return this.httpClient.post(environment.fileController, formData);
  }
}
