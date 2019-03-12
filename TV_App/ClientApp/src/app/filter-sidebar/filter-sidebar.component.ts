import { Component, OnInit } from '@angular/core';
import { QueryParamsService } from '../utilities/query-params.service';
import { ListingService } from '../utilities/listing.service';
import { Time } from '@angular/common';
import { TranslationsService } from '../utilities/translations.service';
import { ISO8601_DATE_REGEX } from '@angular/common/src/i18n/format_date';

@Component({
  selector: 'app-filter-sidebar',
  templateUrl: './filter-sidebar.component.html',
  styleUrls: ['./filter-sidebar.component.scss']
})
export class FilterSidebarComponent implements OnInit {
  constructor(private _queryParams: QueryParamsService,
              private listing_service: ListingService,  
              private _translations: TranslationsService) { }
  timeFrom: Time = { hours: 0, minutes: 0 };
  timeTo: Time  = { hours: 0, minutes: 0 };
  ngOnInit() {  }
  onFromInput($event) {
    this.timeFrom = this.parseTime($event.target.value);
    this._queryParams.hourStart = this.timeFrom;
    this.listing_service.refresh();
  }

  onToInput($event) {
    this.timeTo = this.parseTime($event.target.value);
    this._queryParams.hourEnd = this.timeTo;
    this.listing_service.refresh();
  }
  public getDate(): number{
    return this._queryParams.date;
  }

  public nextDay() {
    this._queryParams.date += (1000 * 60 * 60 * 24);
    this.listing_service.refresh();
  }

  public prevDay() {
    this._queryParams.date -= (1000 * 60 * 60 * 24);
    this.listing_service.refresh();
  }

  private parseTime(input: string): Time {
    return {
      hours: parseInt(input.split(':')[0]),
      minutes: parseInt(input.split(':')[1]),
    };
  }

  public translateDate(initial: string): string {
    return initial
      .replace('Mon', 'Pn')
      .replace('Tue', 'Wt')
      .replace('Wed', 'Śr')
      .replace('Thu', 'Cz')
      .replace('Fri', 'Pt')
      .replace('Sat', 'Sb')
      .replace('Sun', 'Nd')
      .replace('Jan', 'Sty')
      .replace('Feb', 'Lut')
      .replace('Mar', 'Mar')
      .replace('Apr', 'Kwi')
      .replace('May', 'Maj')
      .replace('Jun', 'Cze')
      .replace('Jul', 'Lip')
      .replace('Aug', 'Sie')
      .replace('Sep', 'Wrz')
      .replace('Oct', 'Paź')
      .replace('Nov', 'Lis')
      .replace('Dec', 'Gru');
  }


}
