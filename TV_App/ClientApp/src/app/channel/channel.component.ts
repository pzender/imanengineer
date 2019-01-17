import { Component, OnInit } from '@angular/core';
import { ListingService } from '../utilities/listing.service';
import { HttpClient } from '@angular/common/http';
import { QueryParamsService } from '../utilities/query-params.service';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.scss']
})
export class ChannelComponent implements OnInit {

  constructor(
    private _listingService : ListingService,
    private _route : ActivatedRoute,
    private _queryParams : QueryParamsService,
    private _httpClient : HttpClient
  ) { }

  private listing : IProgrammeListElement[]
  private id : number

  ngOnInit() {
    this.id = this._route.snapshot.params['id']
    this._queryParams.setChannel(this.id)
    this._listingService.getListing().subscribe(result => this.listing = result);
  }

}
