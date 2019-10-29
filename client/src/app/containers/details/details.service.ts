import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/shared/services/user.service';
import { ProgrammesApiService } from 'src/app/shared/services/programmes-api.service';

@Injectable({
  providedIn: 'root'
})
export class DetailsService {

  constructor(private _http: HttpClient, private user: UserService, private api: ProgrammesApiService) { }
  
  public fetch(id, filters): Observable<any[]> {
    return this.api.fetch(`${environment.api}Programmes/${id}/Similar`, filters, this.user.getUser());
  }

  info(id): Observable<any> {
    return this._http.get<any>(`${environment.api}Programmes/${id}`)
  }

}
