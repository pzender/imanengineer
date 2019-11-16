import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { tap } from "rxjs/operators";
import { NotificationsService } from './notifications.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  static USER = 'user';

  getUser(): string{
    return localStorage.getItem(UserService.USER);
  }

  login(username: string): Observable<any> {
    return this._http.get(`${environment.api}Users/${username}`).pipe(
      tap(resp => { 
        localStorage.setItem(UserService.USER, resp['login']); 
        this.push.subscribeToNotifications(resp['login']);
      }),
    )
  }

  logout() {
    localStorage.removeItem(UserService.USER)
    this.push.unsubscribeToNotifications(this.getUser());
  }

  register(username: string): Observable<any> {
    return this._http.post(`${environment.api}Users`, username).pipe(
      tap(resp => { 
        localStorage.setItem(UserService.USER, resp['login']); 
        this.push.subscribeToNotifications(resp['login']);
    }))
  }

  isAnonymous(): boolean {
    return localStorage.getItem(UserService.USER) == null
  }
  
  constructor(
    private _http: HttpClient, 
    private push: NotificationsService
  ) { }
}
