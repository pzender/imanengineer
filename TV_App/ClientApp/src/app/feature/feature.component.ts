import { Component, OnInit } from '@angular/core';
import { ListingService } from '../utilities/listing.service';
import { ActivatedRoute } from '@angular/router';
import { QueryParamsService } from '../utilities/query-params.service';
import { HttpClient } from '@angular/common/http';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';

@Component({
  selector: 'app-feature',
  templateUrl: './feature.component.html',
  styleUrls: ['./feature.component.scss']
})
export class FeatureComponent implements OnInit {

  constructor(
    private _listingService: ListingService,
    private _route: ActivatedRoute,
    private _queryParams: QueryParamsService,
    private _httpClient: HttpClient
  ) { }

  public listing: IProgrammeListElement[];
  public title: string;

  ngOnInit() {
    const id: number = this._route.snapshot.params['id'];
    this._queryParams.setFeature(id);
    this._listingService.getListing().subscribe(result => this.listing = result);
    this._httpClient.get(`/api/Features/${id}`).subscribe(result => this.title = `${result['type']}: ${result['value']}`);
  }

}
