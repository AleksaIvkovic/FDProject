import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Configuration } from '../models/configuration.model';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {

  constructor(private httpClient: HttpClient) { }

  getConfiguration(){
    return this.httpClient.get<Configuration>(environment.configurationController);
  }
}
