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
    private _listingService: ListingService,
    private _route: ActivatedRoute,
    private _queryParams: QueryParamsService,
    private _httpClient: HttpClient
  ) { }

  private listing: IProgrammeListElement[];
  private id: number;
  private title: string;

  ngOnInit() {
    this.id = this._route.snapshot.params['id'];
    this._queryParams.setChannel(this.id);
    this._httpClient.get(`http://localhost:5000/api/Channels/${this.id}`)
      .subscribe(result => this.title = result['name'])
    this._listingService.getListing().subscribe(result => this.listing = result);
  }

}
