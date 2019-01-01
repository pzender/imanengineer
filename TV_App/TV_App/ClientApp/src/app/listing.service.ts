import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IProgrammeList } from './interfaces/ProgrammeListModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})


export class ListingService {

  constructor(private http: HttpClient) { 
    this.requestListing().subscribe(result => this.listingSubject.next(result))
  }

  public getListing() : Observable<IProgrammeList> {
    return this.listingSubject.asObservable()
  }

  private requestListing() : Observable<IProgrammeList> {
    return this.http.get<IProgrammeList>("http://localhost:58289/api/Programmes", 
    { params : {'channel' : this.channel}});
  }

  private channel : string = "HBO"
  private listingSubject : BehaviorSubject<IProgrammeList> = new BehaviorSubject<IProgrammeList>({
    "title":"",
    "listing":[]
  });
}
