import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { NotificationsService } from '../../services/notifications.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private userService: UserService, private notificationService: NotificationsService) { }
  public showLogin: boolean = false;
  public actionResult: {success: boolean, message: string};

  public toggleLogin(){
    this.showLogin = !this.showLogin;
  }

  login(username: string) {
    if(username !== '') {
      this.userService.login(username).subscribe(
        resp => { 
          this.actionResult = { success: true, message: 'Zalogowano' };
        },
        err => { this.actionResult = { success: false, message: 'Użytkownik nie istnieje' } }
      )
      this.showLogin = false;
    }
  }
  create(username: string) {
    if(username !== '') {
      this.userService.register(username).subscribe(
        resp => { this.actionResult = { success: true, message: 'Zalogowano' } },
        err => { this.actionResult = { success: false, message: 'Użytkownik już istnieje' } }
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

  closeWarning(){
    this.actionResult = undefined;
  }

  ngOnInit() {
  }

  logout() {
    this.userService.logout();
  }

}
