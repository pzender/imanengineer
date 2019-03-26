import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public new_name = '';
  public searchterms = '';

  constructor(private _router: Router,
              private _http: HttpClient) { }

  ngOnInit() {
  }

  public getUser(): string {
    return ""
  }

  public setUser(value: string) {
  }

  public userEmpty(): boolean {
    return this.getUser() === '';
  }

  public logout(): void {
    this.setUser('');
  }

  public login(): void {
    this._http
      .post('/api/Users/', this.new_name)
      .subscribe(result =>
        this.setUser(result['login'])
      );
  }

  public search() {
    this._router.navigate(['Search']);
  }
}
