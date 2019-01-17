import { Injectable } from '@angular/core';
import { Time } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class QueryParamsService {

  constructor() { }
  public currentUser : string = ""
  private endpoint : string
  public hourStart : Time = {hours : 0, minutes: 0}
  public hourEnd : Time = {hours : 0, minutes: 0}

  public getUrl() : string{
    if(this.endpoint.endsWith('Recommended'))
      this.setRecommended()
    else if(this.endpoint.endsWith('Ratings'))
      this.setProfile()
    
    return this.endpoint
  }

  public setRecommended() : void {
    this.endpoint = `api/Users/${this.currentUser}/Recommended`
  }

  public setProfile() : void {
    this.endpoint =  `api/Users/${this.currentUser}/Ratings`
  }

  public setChannel(id : number) : void {
    this.endpoint =  `api/Channels/${id}/Programmes`
  }

  public setFeature(id : number) : void {
    this.endpoint =  `api/Features/${id}/Programmes`
  }

  public setProgramme(id : number) : void {
    this.endpoint = `api/Programmes/${id}/Similar`
  }


  public getParams() : any {
    let params = {
      'from' : this.timeString(this.hourStart),
      'to' : this.timeString(this.hourEnd)
    };
    if(!this.endpoint.endsWith('Recommended') && !this.endpoint.endsWith('Profile'))
      params['username'] = this.currentUser;
    
    return params
  }

  private timeString(value : Time){
    return value.hours.toString() + ":" + value.minutes.toString()
  }
}
