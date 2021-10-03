import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {SourcesModel} from '../sources/sources.Model';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SourceService {

  private API_URL = environment.API_URL + 'Source';

  constructor(private http: HttpClient) {
  }

  getSources(): Observable<SourcesModel[]> {
    return this.http.get<SourcesModel[]>(this.API_URL);
  }
}
