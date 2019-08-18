import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserService } from 'src/app/shared/services/user.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  constructor(private _http: HttpClient, private user: UserService) { }
  
  fetch(id, filters): Observable<any[]> {
    let p = {
      ...filters,
      ...{username: this.user.getUser()}
    }

    return this._http.get<any[]>(`${environment.api}Features/${id}/Programmes`, { params: p });
  }

  info(id): Observable<any> {
    return this._http.get<any>(`${environment.api}Features/${id}`)
  }

}
