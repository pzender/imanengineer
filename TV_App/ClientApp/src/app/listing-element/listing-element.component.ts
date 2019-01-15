import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';
import { HttpClient } from '@angular/common/http';
import { QueryParamsService } from '../utilities/query-params.service';


@Component({
  selector: 'app-listing-element',
  templateUrl: './listing-element.component.html',
  styleUrls: ['./listing-element.component.scss']
})
export class ListingElementComponent implements OnInit {
  @Output() buttonClicked = new EventEmitter<string>();
  @Input('programme') programme : IProgrammeListElement
  constructor(private _http : HttpClient,
              private _queryParams : QueryParamsService) { }

  feat_types() : string[] {
    return this.programme.features
      .map(function(f : {type : string, value : string}){ return f.type})
      .filter(this.onlyUnique)
  };

  parse_date(iso_date : string) {
    return Date.parse(iso_date);
  }

  feat_array(desired_type : string) : {"id" : number, "name" : string}[] {
    let chosen_feats = this.programme.features
      .filter(function(f : {id : number, type : string, value : string}){ return f.type === desired_type})
      .map(function(f : {id : number, type : string, value : string}){ return {
        "id" : f.id, 
        "name" : f.value
      }})
    return chosen_feats;
  };

  rateButton(value : number){
    this._http.post(
      "/api/Users/"+this._queryParams.currentUser+"/Ratings", 
      {programme_id : this.programme.id, rating_value : value},
      {responseType : 'json'}
    )
    .subscribe(result => this.buttonClicked.emit(result.toString()))
  }

  ngOnInit() {
  }

  onlyUnique(value : any, index : number, self : any[]) { 
    return self.indexOf(value) === index;
  }

  translateDate(initial : string) : string {
    return initial
      .replace("Mon", "Pn")
      .replace("Tue", "Wt")
      .replace("Wed", "Śr")
      .replace("Thu", "Cz")
      .replace("Fri", "Pt")
      .replace("Sat", "Sb")
      .replace("Sun", "Nd")
      .replace("Jan", "Sty")
      .replace("Feb", "Lut")
      .replace("Mar", "Mar")
      .replace("Apr", "Kwi")
      .replace("May", "Maj")
      .replace("Jun", "Cze")
      .replace("Jul", "Lip")
      .replace("Aug", "Sie")
      .replace("Sep", "Wrz")
      .replace("Oct", "Paź")
      .replace("Nov", "Lis")
      .replace("Dec", "Gru")
  }
}