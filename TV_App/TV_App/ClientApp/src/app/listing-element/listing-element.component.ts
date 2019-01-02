import { Component, OnInit, Input } from '@angular/core';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';


@Component({
  selector: 'app-listing-element',
  templateUrl: './listing-element.component.html',
  styleUrls: ['./listing-element.component.scss']
})
export class ListingElementComponent implements OnInit {
  @Input('programme') programme : IProgrammeListElement
  constructor() { }

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


  ngOnInit() {
  }

  onlyUnique(value, index, self) { 
    return self.indexOf(value) === index;
  }
}
