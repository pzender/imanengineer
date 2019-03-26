import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Time } from '@angular/common';
import { ISO8601_DATE_REGEX } from '@angular/common/src/i18n/format_date';

@Component({
  selector: 'app-filter-sidebar',
  templateUrl: './filter-sidebar.component.html',
  styleUrls: ['./filter-sidebar.component.scss']
})
export class FilterSidebarComponent implements OnInit {

  @Output() filtersChanged = new EventEmitter<{from: string, to: string, date: number}>();

  constructor() { }
  timeFrom: string;
  timeTo: string;
  currentDate: number = Date.now();
  ngOnInit() {  }
  onFromInput($event) {
    this.timeFrom = ($event.target.value);
    console.log($event.target.value)
    this.filtersChanged.emit({'from': this.timeFrom, 'to': this.timeTo, 'date': this.currentDate})
  }

  onToInput($event) {
    this.timeTo = ($event.target.value);
    this.filtersChanged.emit({'from': this.timeFrom, 'to': this.timeTo, 'date': this.currentDate})

  }

  public nextDay() {
    this.currentDate += (1000 * 60 * 60 * 24);
    this.filtersChanged.emit({'from': this.timeFrom, 'to': this.timeTo, 'date': this.currentDate});

  }

  public prevDay() {
    this.currentDate -= (1000 * 60 * 60 * 24);
    this.filtersChanged.emit({'from': this.timeFrom, 'to': this.timeTo, 'date': this.currentDate})

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
