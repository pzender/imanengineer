import { Component, OnInit } from '@angular/core';
import { QueryParamsService } from '../utilities/query-params.service';
import { ListingService } from '../utilities/listing.service';
import { Time } from '@angular/common';

@Component({
  selector: 'app-filter-sidebar',
  templateUrl: './filter-sidebar.component.html',
  styleUrls: ['./filter-sidebar.component.scss']
})
export class FilterSidebarComponent implements OnInit {

  constructor(private _queryParams : QueryParamsService, 
              private listing_service : ListingService) { }

  ngOnInit() {
  }
  timeFrom : Time = {hours : 0, minutes: 0}
  timeTo : Time  = {hours : 23, minutes: 59}
  onFromInput($event){
    this.timeFrom = this.parseTime($event.target.value);
    this._queryParams.hourStart = this.timeFrom;
    this.listing_service.refresh();
  }

  onToInput($event){
    this.timeTo = this.parseTime($event.target.value);
    this._queryParams.hourEnd = this.timeTo;
    this.listing_service.refresh();
  }



  private parseTime(input : string) : Time{
    return {
      hours : parseInt(input.split(':')[0]),
      minutes : parseInt(input.split(':')[1]),
    }
  }
}
