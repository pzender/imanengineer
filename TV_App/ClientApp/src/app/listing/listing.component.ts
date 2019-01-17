import { Component, OnInit, Input } from '@angular/core';
import { IProgrammeList } from '../interfaces/ProgrammeListModel';
import { ListingService } from '../utilities/listing.service';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';
import { HttpClient } from '@angular/common/http';
import { QueryParamsService } from '../utilities/query-params.service';


@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrls: ['./listing.component.scss']
})
export class ListingComponent implements OnInit {

  //private 

  constructor(private _listingService : ListingService,
              private _httpClient : HttpClient,
              private _queryParams : QueryParamsService) 
    {

    }

  ngOnInit() {
    
    //this._listingService.getListing().subscribe(result => this.listing = result);
  }

  @Input('listing') listing : IProgrammeListElement[]

  title : string
  //listing : IProgrammeListElement[] = []

  show_details() : boolean {
    return true
  }

  show_listing() : boolean {
    return false
  }

  onButtonClicked(response : string){
    this._listingService.getListing().subscribe(result => this.listing = result);
  }
}
