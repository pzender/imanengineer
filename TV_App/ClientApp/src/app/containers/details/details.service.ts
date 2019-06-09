import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/utilities/user.service';

@Injectable({
  providedIn: 'root'
})
export class DetailsService {

  constructor(private _http: HttpClient, private user: UserService) { }
  
  fetch(id, filters): Observable<any[]> {
    console.log(filters)
    let p = {
      ...filters,
      ...{username: this.user.getUser()}
    }
    console.log(p)

    return this._http.get<any[]>(`${environment.api}Programmes/${id}/Similar`, { params: p });
  }

  info(id): Observable<any> {
    return this._http.get<any>(`${environment.api}Programmes/${id}`)
  }

}
