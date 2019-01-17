import { Component, OnInit } from '@angular/core';
import { QueryParamsService } from '../utilities/query-params.service';
import { ListingService } from '../utilities/listing.service';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public new_name = '';
  constructor(private _queryParams: QueryParamsService,
              private _listingService: ListingService,
              private _http: HttpClient) { }

  ngOnInit() {
  }

  public getUser(): string {
    return this._queryParams.getCurrentUser();
  }

  public setUser(value: string) {
    this._queryParams.setCurrentUser(value);
    this._listingService.refresh();
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

}
