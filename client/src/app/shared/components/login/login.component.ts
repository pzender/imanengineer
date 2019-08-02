import { Component, OnInit } from '@angular/core';
import { ngSubmit } from "@angular/forms";
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private userService: UserService) { }
  public showLogin :boolean = false;

  public toggleLogin(){
    this.showLogin = !this.showLogin;
    console.log(this.showLogin)
  }

  login(username: string) {
    if(username !== '') {
      this.userService.login(username).subscribe(
        resp => { console.log(resp) },
        err => { console.log(err) }
      )
      this.showLogin = false;
    }
  }

  create(username: string) {
    if(username !== '') {
      this.userService.register(username).subscribe(
        resp => { console.log(resp) },
        err => { console.log(err) }
      )
      this.showLogin = false;
    }
  }


  getUsername(): string {
    return this.userService.getUser()
  }

  isLoggedIn(): boolean {
    return !this.userService.isAnonymous()
  }

  ngOnInit() {
  }

  logout() {
    this.userService.logout();
  }

}
