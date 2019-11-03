import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { NotificationsService } from '../../services/notifications.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private userService: UserService, 
    private modalService: NgbModal
  ) { }
  public showLogin: boolean = false;
  public actionResult: {success: boolean, message: string};
  closeResult: string;
  
  public toggleLogin(){
    this.showLogin = !this.showLogin;
  }

  open(content) {
    this.modalService.open(
      content, 
      {
        ariaLabelledBy: 'modal-basic-title', 
        windowClass: 'dark-modal'
      }
    ).result.then(
      (result) => { 
        if(result.action === 'login') this.login(result.name)
        if(result.action === 'create') this.create(result.name)
      }, 
      (reason) => { console.log(reason); } 
    );
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }

  login(username: string) {
    if(username !== '') {
      this.userService.login(username).subscribe(
        resp => { 
          this.actionResult = { success: true, message: 'Zalogowano' };
        },
        err => { this.actionResult = { success: false, message: 'Użytkownik nie istnieje' } }
      )
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
