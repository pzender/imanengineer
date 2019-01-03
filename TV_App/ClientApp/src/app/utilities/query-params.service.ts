import { Injectable } from '@angular/core';
import { Time } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class QueryParamsService {

  constructor() { }
  public currentUser : string = "Przemek"
  public endpoint : {feature : string, id : number} 
  public hourStart : Time = {hours : 0, minutes: 0}
  public hourEnd : Time = {hours : 0, minutes: 0}

  public getUrl() : string{
    let middle = this.endpoint.feature == undefined 
              && this.endpoint.id == undefined
      ? "" 
      : "/" + this.endpoint.feature +"/"+ this.endpoint.id;
    return "api" + middle +"/Programmes";
  }

  public getParams() : any {
    console.log(this.timeString(this.hourStart));
    return {
      'username' : this.currentUser,
      'from' : this.timeString(this.hourStart),
      'to' : this.timeString(this.hourEnd)
    };    
  }

  private timeString(value : Time){
    return value.hours.toString() + ":" + value.minutes.toString()
  }
}
