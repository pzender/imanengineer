import { Component, OnInit, ViewChild } from '@angular/core';
import { DetailsService } from './details.service';
import { ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';
import { debounceTime } from 'rxjs/operators';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  @ViewChild('sidebar') sidebar: FilterSidebarComponent

  constructor(private service: DetailsService, private route: ActivatedRoute) { }

  listing: any[];
  requestStatus: string = 'waiting';
  title: string
  id: number;
  info: any;
  filters: {from: Time, to: Time, date: number};

  updateFilters($event){
    this.filters = $event;
    this.fetch();
  }

  fetch(){
    this.requestStatus = "waiting";
    this.service.info(this.id).subscribe(
      response => { this.info = response; }
    )
    this.service.fetch(this.id, this.filters).subscribe(
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
        this.id = value[0]['id'];
        this.updateFilters(value[1])
      })

  }

}
