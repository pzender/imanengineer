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
    this.requestListing().subscribe(result => this.listingSubject.next(result))
  }

  public getListing() : Observable<IProgrammeListElement[]> {
    return this.listingSubject.asObservable()
  }

  private requestListing() : Observable<IProgrammeListElement[]> {
    return this.http.get<IProgrammeListElement[]>("http://localhost:58289/api/Programmes", 
    { params : {'username' : 'Przemek'}});
  }

  private channel : string = "HBO"
  private listingSubject : BehaviorSubject<IProgrammeListElement[]> = new BehaviorSubject<IProgrammeListElement[]>([]);
}
