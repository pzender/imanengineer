import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/shared/services/user.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private _http: HttpClient, private user: UserService) { }
  
  fetch(filters): Observable<any[]> {
    let p = {
      ...filters,
      ...{username: this.user.getUser()}
    }
    return this._http.get<any[]>(`${environment.api}Users/${this.user.getUser()}/Notifications`, { params: p });
  }
}
