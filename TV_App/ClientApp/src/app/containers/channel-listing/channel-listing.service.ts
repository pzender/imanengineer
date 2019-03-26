import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChannelListingService {

  constructor(private _http: HttpClient) { }

  fetch(id): Observable<any[]> {
    return this._http.get<any[]>(`${environment.api}Channels/${id}/Programmes`)
  }

  info(id): Observable<any> {
    return this._http.get<any>(`${environment.api}Channels/${id}`)
  }
}
