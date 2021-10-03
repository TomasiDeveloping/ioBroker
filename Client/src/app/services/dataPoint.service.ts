import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {DataPointModel} from '../dataPoints/dataPoint.Model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DataPointService {
  private API_URL = environment.API_URL + 'DataPoint';

  constructor(public http: HttpClient) {
  }

  getDataPoints(): Observable<DataPointModel[]> {
    return this.http.get<DataPointModel[]>(this.API_URL);
  }
}
