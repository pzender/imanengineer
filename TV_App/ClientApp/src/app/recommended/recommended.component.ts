import { Component, OnInit } from '@angular/core';
import { ListingService } from '../utilities/listing.service';
import { ActivatedRoute } from '@angular/router';
import { QueryParamsService } from '../utilities/query-params.service';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';

@Component({
  selector: 'app-recommended',
  templateUrl: './recommended.component.html',
  styleUrls: ['./recommended.component.scss']
})
export class RecommendedComponent implements OnInit {

  constructor(
    private _listingService: ListingService,
    private _route: ActivatedRoute,
    private _queryParams: QueryParamsService,
  ) { }

  private listing: IProgrammeListElement[];
  public isLogged(): boolean {
    return this._queryParams.getCurrentUser() !== '';
  }
  private listingEmpty(): boolean {
    return this.listing.length === 0;
  }
  ngOnInit() {
    this._queryParams.setRecommended();
    this._listingService.getListing().subscribe(result => this.listing = result);
  }

}
