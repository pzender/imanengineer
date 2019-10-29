import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/shared/services/user.service';
import { HttpClient } from '@angular/common/http';
import { ProgrammesApiService } from 'src/app/shared/services/programmes-api.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService { 
  constructor(private api: ProgrammesApiService, private user: UserService) { }
  
  public fetch(filters): Observable<any[]> {
    return this.api.fetch(`${environment.api}Users/${this.user.getUser()}/Notifications`, filters, this.user.getUser());
  }
}
