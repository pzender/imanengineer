import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
//import { IProgrammeList } from './interfaces/ProgrammeListModel';
import { HttpClient } from '@angular/common/http';
import { IProgrammeListElement } from './interfaces/ProgrammeListElement';

@Injectable({
  providedIn: 'root'
})


export class ListingService {


  constructor(private http: HttpClient) { 
    //this.requestListing().subscribe(result => this.listingSubject.next(result))
  }

  public getListing() : Observable<IProgrammeListElement[]> {
    return this.listingSubject.asObservable()
  }

  public setEndpoint(value : string) : void {
    this.endpoint = value
  }

  public setId(value : number) : void {
    this.id = value
    this.requestListing().subscribe(result => this.listingSubject.next(result))
  }

  private requestListing() : Observable<IProgrammeListElement[]> {
    let url = "http://localhost:52153/api/";
    if(this.endpoint != null && this.id != null){
      url += this.endpoint + "/" + this.id + "/"
    }
    console.log(url + "Programmes", { params : {'username' : 'Przemek'}})
    return this.http.get<IProgrammeListElement[]>(url + "Programmes", 
    { params : {'username' : 'Przemek'}});
  }

  private endpoint : string
  private id : number
  private listingSubject : BehaviorSubject<IProgrammeListElement[]> = new BehaviorSubject<IProgrammeListElement[]>([]);
}
