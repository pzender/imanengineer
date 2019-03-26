import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  static USER = 'user';

  getUser(): string{
    return localStorage.getItem(UserService.USER);
  }

  setUser(value: string): void{
    localStorage.setItem(UserService.USER, value);
  }

  isAnonymous(): boolean{
    return localStorage.getItem(UserService.USER) != undefined;
  }
  constructor() { }
}
