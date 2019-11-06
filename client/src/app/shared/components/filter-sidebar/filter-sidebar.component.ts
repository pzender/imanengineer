import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { Time } from '@angular/common';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-filter-sidebar',
  templateUrl: './filter-sidebar.component.html',
  styleUrls: ['./filter-sidebar.component.scss']
})
export class FilterSidebarComponent implements OnInit {
  @Output() filtersChanged = new EventEmitter<any>();
  @Input() showTime: boolean = true;
  @Input() showOffer: boolean = true;

  constructor(private router: Router) { }
  ONE_DAY: number = 1000 * 60 * 60 * 24;
  searchterm: string;
  timeFrom: string = this.timeOfDay();
  timeTo: string = '00:00';
  currentDate: number = Date.now();
  currentOffer: number = 0;
  currentTheme: number = 0;
  timeFromDefault = true;

  ngOnInit() { 
    this.filtersChanged.emit(this.buildFilter())
  }

  timeOfDay() : string {
    return `${new Date().getHours()}:${new Date().getMinutes()}`;
  }

  onFromInput($event) {
    this.timeFromDefault = false;
    this.timeFrom = ($event.target.value);
    this.filtersChanged.emit(this.buildFilter());
  }

  onToInput($event) {
    this.timeTo = ($event.target.value);
    this.filtersChanged.emit(this.buildFilter());
  }

  public moveDay(value: number) {
    if(this.timeFromDefault) this.timeFrom = '00:00';

    this.currentDate += (value * this.ONE_DAY);
    this.filtersChanged.emit(this.buildFilter());
  }

  public offerPicked(value: number) {
    this.currentOffer = value;
    this.filtersChanged.emit(this.buildFilter());
  }

  public themePicked(value: number) {
    this.currentTheme = value;
    this.filtersChanged.emit(this.buildFilter());
  }

  public search() {
    this.router.navigate(['Search', this.searchterm]);
  }

  private buildFilter() {
    let filter = {};
    if (this.showTime) {
      filter = {
        ...filter,
        'from': this.timeFrom, 
        'to': this.timeTo, 
        'date': this.currentDate
      };
    }
    if (this.showOffer) {
      filter = {
        ...filter,
        'offer_id': this.currentOffer,
        'theme_id': this.currentTheme
      };
    }
    return filter;
  }
}
