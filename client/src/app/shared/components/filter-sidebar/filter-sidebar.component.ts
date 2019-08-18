import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Time } from '@angular/common';

@Component({
  selector: 'app-filter-sidebar',
  templateUrl: './filter-sidebar.component.html',
  styleUrls: ['./filter-sidebar.component.scss']
})
export class FilterSidebarComponent implements OnInit {

  @Output() filtersChanged = new EventEmitter<any>();
  @Input() showTime: boolean = true;
  @Input() showOffer: boolean = true;

  constructor() { }
  ONE_DAY: number = 1000 * 60 * 60 * 24;
  
  timeFrom: string;
  timeTo: string;
  currentDate: number = Date.now();
  currentOffer: number = 0;
  



  ngOnInit() { 
    this.filtersChanged.emit(this.buildFilter())
  }
  onFromInput($event) {
    this.timeFrom = ($event.target.value);
    this.filtersChanged.emit(this.buildFilter())
  }

  onToInput($event) {
    this.timeTo = ($event.target.value);
    this.filtersChanged.emit(this.buildFilter())

  }

  public nextDay() {
    this.currentDate += this.ONE_DAY;
    this.filtersChanged.emit(this.buildFilter());

  }

  public prevDay() {
    this.currentDate -= this.ONE_DAY;
    this.filtersChanged.emit(this.buildFilter())
  }

  public offerPicked(value: number) {
    this.currentOffer = value;
    this.filtersChanged.emit(this.buildFilter())
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
        'offer_id': this.currentOffer
      };
    }
    return filter;
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
