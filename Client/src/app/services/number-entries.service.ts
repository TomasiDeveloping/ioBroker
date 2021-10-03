import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {NumberEntryModel} from '../core/numberEntry.Model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NumberEntriesService {

  private API_URL = environment.API_URL + 'NumberEntries/';

  constructor(private http: HttpClient) {
  }

  getEntriesByParamsAndId(dataPointId: number, pageSize: number): Observable<NumberEntryModel[]> {
    let params = new HttpParams();
    params = params.set('pageSize', pageSize);
    return this.http.get<NumberEntryModel[]>(this.API_URL + 'GetEntriesByIdAndParams/' + dataPointId, {params});
  }

  updateEntry(entry: NumberEntryModel): Observable<NumberEntryModel> {
    return this.http.put<NumberEntryModel>(this.API_URL + entry.id, entry);
  }

  deleteEntry(entry: NumberEntryModel): Observable<any> {
    return this.http.delete<boolean>(this.API_URL + entry.id, {body: entry});
  }
}
