import { Component, OnInit } from '@angular/core';
import { IProgrammeList } from '../interfaces/ProgrammeListModel';
import { ListingService } from '../utilities/listing.service';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { QueryParamsService } from '../utilities/query-params.service';


@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrls: ['./listing.component.scss']
})
export class ListingComponent implements OnInit {
  constructor(private _listingService : ListingService,
              private _route : ActivatedRoute,
              private _httpClient : HttpClient,
              private _queryParams : QueryParamsService) 
    {
      if(this._route.snapshot.params.feature === "Programmes"){
        this.title = "Podobne programy"
        this.details_id = this._route.snapshot.params.id
      }
      else if(this._route.snapshot.params.feature === "Channels"){
        this.title = "Na kanale "
        this._httpClient
          .get<{id : number, name : string}>("/api/Channels/"+this._route.snapshot.params.id)
          .subscribe(result => this.title.concat(result.name))
      }
      else if(this._route.snapshot.params.feature === "Features"){
        this._httpClient
          .get<{id : number, value : string, type : string}>(this._route.snapshot.root.url +"/api/Features/"+this._route.snapshot.params.id)
          .subscribe(result => this.title = result.value)
      }
      else {
        if(this._queryParams.currentUser != ""){
          this.title = "Twoje rekomendacje"
        }
        else {
          this.title = ""
        }
      }
  
      this._queryParams.endpoint = {
        feature : this._route.snapshot.params.feature,
        id : this._route.snapshot.params.id
      };
      this._listingService.getListing().subscribe(result => this.listing = result);
    }

  ngOnInit() {

  }
  details_id : number
  title : string
  listing : IProgrammeListElement[] = []

  show_details() : boolean {
    return this._route.snapshot.params.feature === "Programmes"
  }

  show_listing() : boolean {
    return this._route.snapshot.params.feature === "Programmes" ||
           this._route.snapshot.params.feature === "Channels" ||
           this._route.snapshot.params.feature === "Features" ||
           this._queryParams.currentUser != ""
  }

  onButtonClicked(response : string){
    this._listingService.getListing().subscribe(result => this.listing = result);
  }
}
