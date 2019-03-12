import { Component, OnInit } from '@angular/core';
import { ListingService } from '../utilities/listing.service';
import { ActivatedRoute } from '@angular/router';
import { QueryParamsService } from '../utilities/query-params.service';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';


@Component({
  selector: 'app-programme',
  templateUrl: './programme.component.html',
  styleUrls: ['./programme.component.scss']
})
export class ProgrammeComponent implements OnInit {

  constructor(
    private _listingService: ListingService,
    private _route: ActivatedRoute,
    private _queryParams: QueryParamsService,
  ) { }

  public listing: IProgrammeListElement[];
  public id: number;

  ngOnInit() {
    this.id = this._route.snapshot.params['id'];
    this._queryParams.setProgramme(this.id);
    this._listingService.getListing().subscribe(result => this.listing = result);
  }

}
