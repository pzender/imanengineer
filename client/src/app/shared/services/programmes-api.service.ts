import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';
import { HttpClient } from '@angular/common/http';
import { tap, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProgrammesApiService {

  constructor(private http: HttpClient) { }

  public fetch(url: string, filters: any, username: string): Observable<IProgrammeListElement[]> {
    let filtersWithUsername = {
      ...filters,
      ...{ username: username }
    }
    return this.http.get<IProgrammeListElement[]>(url, { params: filtersWithUsername }).pipe(
      map(value => value.map( element => ({
        ...element,
        ...{ emissions: element.emissions || [] }
      }))),
    );
  }
}
