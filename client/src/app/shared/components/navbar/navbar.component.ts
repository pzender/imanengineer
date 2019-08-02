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

  public isLoggedIn(): boolean {
    return !this.service.isAnonymous()
  }

  public search() {
    this._router.navigate(['Search']);
  }
}
