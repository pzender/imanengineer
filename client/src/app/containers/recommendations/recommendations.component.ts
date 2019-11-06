import { Component, OnInit, ViewChild } from '@angular/core';
import { RecommendationsService } from './recommendations.service';
import { Time } from '@angular/common';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-recommendations',
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.scss']
})
export class RecommendationsComponent implements OnInit {
  @ViewChild('sidebar') sidebar: FilterSidebarComponent
  requestStatus: string = 'waiting';

  constructor(private service: RecommendationsService) { }

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
    this.service.fetch(this.filters).subscribe(
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
    this.sidebar.filtersChanged
      .pipe(debounceTime(500))
      .subscribe(ev => this.updateFilters(ev));
  }
}
