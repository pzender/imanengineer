import { Component, OnInit, ViewChild } from '@angular/core';
import { FeatureService } from './feature.service';
import { ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { debounceTime } from 'rxjs/operators';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-feature',
  templateUrl: './feature.component.html',
  styleUrls: ['./feature.component.scss']
})
export class FeatureComponent implements OnInit {
  @ViewChild('sidebar') sidebar: FilterSidebarComponent

  constructor(private service: FeatureService, private route: ActivatedRoute) { }

  listing: any[];
  requestStatus: string = 'waiting';
  title: string
  id: number;
  filters: {from: Time, to: Time, date: number};

  updateFilters($event){
    this.filters = $event;
    this.fetch();
  }

  fetch(){
    this.requestStatus = "waiting";
    this.service.fetch(this.id, this.filters).subscribe(
      response => { 
        this.listing = response; 
        this.requestStatus = response.length > 0 ? "success" : "empty";
      },
      error => {
        this.requestStatus = "failure";
      }
    )
    this.service.info(this.id).subscribe(
      response => { this.title = response.value; }
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
