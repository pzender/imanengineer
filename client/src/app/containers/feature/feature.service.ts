import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserService } from 'src/app/shared/services/user.service';
import { environment } from 'src/environments/environment';
import { ProgrammesApiService } from 'src/app/shared/services/programmes-api.service';

@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  constructor(private _http: HttpClient, private user: UserService, private api: ProgrammesApiService) { }
  
  public fetch(id, filters): Observable<any[]> {
    return this.api.fetch(`${environment.api}Features/${id}/Programmes`, filters, this.user.getUser());
  }

  info(id): Observable<any> {
    return this._http.get<any>(`${environment.api}Features/${id}`)
  }

}
