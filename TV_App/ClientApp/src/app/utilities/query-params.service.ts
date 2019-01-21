import { Injectable } from '@angular/core';
import { Time } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class QueryParamsService {

  constructor() {
    this.date = Date.now();
    //this.date.setDate(Date.now())
  }
  private endpoint: string;
  public hourStart: Time = {hours : 0, minutes: 0};
  public hourEnd: Time = {hours : 0, minutes: 0};
  public date: number;
  public searchterm = '';

  public getCurrentUser(): string {
    const currentUser = localStorage.getItem('currentUser');
    return currentUser !== null ? currentUser : '';
  }

  public setCurrentUser(value: string): void {
    localStorage.setItem('currentUser', value);
  }

  public getUrl(): string {
    if (this.endpoint.endsWith('Recommended')) {
      this.setRecommended();
    } else
    if (this.endpoint.endsWith('Ratings')) {
      this.setProfile();
    }
    return this.endpoint;
  }

  public setSearch(): void {
    this.endpoint = `api/Programmes`;
  }

  public setRecommended(): void {
    this.searchterm = '';
    this.endpoint = `api/Users/${this.getCurrentUser()}/Recommended`;
  }

  public setProfile(): void {
    this.searchterm = '';
    this.endpoint =  `api/Users/${this.getCurrentUser()}/Ratings`;
  }

  public setChannel(id: number): void {
    this.searchterm = '';
    this.endpoint =  `api/Channels/${id}/Programmes`;
  }

  public setFeature(id: number): void {
    this.searchterm = '';
    this.endpoint =  `api/Features/${id}/Programmes`;
  }

  public setProgramme(id: number): void {
    this.searchterm = '';
    this.endpoint = `api/Programmes/${id}/Similar`;
  }

  public getParams(): any {
    const params = {
      'from' : this.timeString(this.hourStart),
      'to' : this.timeString(this.hourEnd),
      'date' : this.date
    };
    if (!this.endpoint.endsWith('Recommended') && !this.endpoint.endsWith('Profile')) {
      params['username'] = this.getCurrentUser();
    }
    if (this.searchterm !== '') {
      params['search'] = this.searchterm;
    }
    return params;
  }

  private timeString(value: Time) {
    return value.hours.toString() + ':' + value.minutes.toString();
  }

}
