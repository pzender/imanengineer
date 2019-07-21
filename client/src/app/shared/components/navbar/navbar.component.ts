import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public new_name = '';
  public searchterms = '';

  constructor(private _router: Router,
              private _http: HttpClient,
              private service: UserService) { }

  ngOnInit() {
  }

  public getUser(): string {
    return this.service.getUser();
  }

  public setUser(value: string) {
    this.service.setUser(value);
  }

  public userEmpty(): boolean {
    return this.getUser() === '';
  }

  public logout(): void {
    this.setUser(undefined);
  }

  public login(): void {
    this.setUser(this.new_name)
  }

  public search() {
    this._router.navigate(['Search']);
  }
}
