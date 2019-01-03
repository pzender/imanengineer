import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';
import { QueryParamsService } from './query-params.service';

@Injectable({
  providedIn: 'root'
})


export class ListingService {


  constructor(private http : HttpClient,
              private query_params : QueryParamsService) { 
  }

  public refresh() : void {
    this.requestListing().subscribe(result => this.listingSubject.next(result))
  }

  public getListing() : Observable<IProgrammeListElement[]> {
    this.requestListing().subscribe(result => this.listingSubject.next(result))
    return this.listingSubject.asObservable()
  }

  private requestListing() : Observable<IProgrammeListElement[]> {
    return this.http.get<IProgrammeListElement[]>(this.query_params.getUrl(), 
      { params : this.query_params.getParams()}
    );
  }

  private listingSubject : BehaviorSubject<IProgrammeListElement[]> = new BehaviorSubject<IProgrammeListElement[]>([]);
}
