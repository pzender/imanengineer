import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, forkJoin, merge, combineLatest } from 'rxjs';
import { ProgrammesApiService } from 'src/app/shared/services/programmes-api.service';
import { Time } from '@angular/common';
import { SearchService } from './search.service';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { tap, mergeAll, withLatestFrom, debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  @ViewChild('sidebar') sidebar: FilterSidebarComponent
  constructor(private route: ActivatedRoute, private service: SearchService) { }
  searchterm: string;

  requestStatus: string = 'waiting';

  listing: any[];
  error: {status: number, message: string};
  title: string
  id: number;
  filters: {from: Time, to: Time, date: number};

  updateFilters($event){
    this.filters = $event;
    this.fetch();
  }

  fetch(){
    this.requestStatus = "waiting";
    this.service.fetch(this.filters, this.searchterm).subscribe(
      response => { 
        this.listing = response; 
        this.requestStatus = response.length > 0 ? "success" : "empty";
      },
      error => {
        this.requestStatus = "failure";
      }
    )
  }

  ngOnInit() { 
    combineLatest(this.route.params, this.sidebar.filtersChanged.asObservable())
      .pipe(debounceTime(500))
      .subscribe(value => {
        this.searchterm = value[0]['term'];
        this.updateFilters(value[1])
      })
 }
}
