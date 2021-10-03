import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {StrinEntryModel} from '../core/strinEntry.Model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StringEntriesService {

  private API_URL = environment.API_URL + 'StringEntries/';

  constructor(private http: HttpClient) {
  }

  getEntriesByIdAndParams(dataPointId: number, pageSize: number): Observable<StrinEntryModel[]> {
    let params = new HttpParams();
    params = params.set('pageSize', pageSize);
    return this.http.get<StrinEntryModel[]>(this.API_URL + 'GetEntriesByIdAndParams/' + dataPointId, {params});
  }
}
