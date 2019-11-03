import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/shared/services/user.service';
import { ProgrammesApiService } from 'src/app/shared/services/programmes-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private user: UserService, private api: ProgrammesApiService) { }
  public fetch(filters, term): Observable<any[]> {
    let search = {
      ...filters,
      'search': term
    }
    return this.api.fetch(`${environment.api}Programmes`, search, this.user.getUser());
  }

}
