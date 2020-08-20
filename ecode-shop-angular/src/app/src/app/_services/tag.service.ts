import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError, Observable } from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import { API_URL } from './../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  constructor(private http: HttpClient) { }

  getTags = () : Observable<any[]> => {
    return this.http.get(`${API_URL}/tags`)
    .pipe(map(x => <[]>x), catchError(err => []))
  }

  saveTag (objTag) {
    return this.http.post(`${API_URL}/tags`, objTag);
  }

}
