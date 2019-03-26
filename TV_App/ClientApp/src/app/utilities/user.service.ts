import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  static USER = 'user';

  getUser(): string{
    return localStorage.getItem(UserService.USER);
  }

  setUser(value: string): void{
    this._http.post(`${environment.api}Users`, value)
      .subscribe(result => {
        localStorage.setItem(UserService.USER, result['login']);
      });
  }

  isAnonymous(): boolean{
    return localStorage.getItem(UserService.USER) === "";
  }
  constructor(private _http: HttpClient) { }
}
