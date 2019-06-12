import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/utilities/user.service';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RecommendationsService {
  public fetch(filters): Observable<any[]> {
    console.log(filters)
    let p = {
      ...filters,
      ...{username: this.user.getUser()}
    }
    console.log(p)

    return this._http.get<any[]>(`${environment.api}Users/${this.user.getUser()}/Recommended`, { params: p });
  }

  constructor(private user: UserService, private _http: HttpClient) { }
}
