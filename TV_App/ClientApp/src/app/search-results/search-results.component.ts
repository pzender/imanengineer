import { Component, OnInit } from '@angular/core';
import { ListingService } from '../utilities/listing.service';
import { ActivatedRoute } from '@angular/router';
import { QueryParamsService } from '../utilities/query-params.service';
import { HttpClient } from '@angular/common/http';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss']
})
export class SearchResultsComponent implements OnInit {
  constructor(
    private _listingService: ListingService,
    private _route: ActivatedRoute,
    private _queryParams: QueryParamsService,
    private _httpClient: HttpClient
  ) { }

  private listing: IProgrammeListElement[];
  private title: string;

  ngOnInit() {
    const id: number = this._route.snapshot.params['id'];
    this._queryParams.setSearch();
    this._listingService.getListing().subscribe(result => this.listing = result);
    // this._httpClient.get(`/api/Features/${id}`).subscribe(result => this.title = `${result['type']}: ${result['value']}`);
  }

}
