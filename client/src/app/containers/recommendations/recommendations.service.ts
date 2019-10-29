import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ProgrammesApiService } from 'src/app/shared/services/programmes-api.service';

@Injectable({
  providedIn: 'root'
})
export class RecommendationsService {
  public fetch(filters): Observable<any[]> {
    return this.api.fetch(`${environment.api}Users/${this.user.getUser()}/Recommended`, filters, this.user.getUser());
  }

  constructor(private user: UserService, private api: ProgrammesApiService) { }
}
