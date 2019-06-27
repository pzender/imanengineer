import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';

@Injectable({
  providedIn: 'root'
})
export class ChannelListingService {

  constructor(private _http: HttpClient, private user: UserService) { }

  fetch(id, filters): Observable<any[]> {
    console.log(filters)
    let p = {
      ...filters,
      ...{username: this.user.getUser()}
    }
    console.log(p)

    return this._http.get<any[]>(`${environment.api}Channels/${id}/Programmes`, { params: p });
  }

  info(id): Observable<any> {
    return this._http.get<any>(`${environment.api}Channels/${id}`)
  }
}
