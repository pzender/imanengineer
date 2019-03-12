import { Component, OnInit } from '@angular/core';
import { ListingService } from '../utilities/listing.service';
import { ActivatedRoute } from '@angular/router';
import { QueryParamsService } from '../utilities/query-params.service';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(
    private _listingService: ListingService,
    private _route: ActivatedRoute,
    private _queryParams: QueryParamsService,
  ) { }

  private listing: IProgrammeListElement[];
  public isLogged(): boolean {
    return this._queryParams.getCurrentUser() !== '';
  }

  public listingEmpty(): boolean {
    return this.listing.length === 0;
  }

  ngOnInit() {
    this._queryParams.setProfile();
    this._listingService.getListing().subscribe(result => this.listing = result);
  }

}
