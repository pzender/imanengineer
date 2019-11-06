import { Component, OnInit, ViewChild } from '@angular/core';
import { ChannelListingService } from './channel-listing.service';
import { ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-channel-listing',
  templateUrl: './channel-listing.component.html',
  styleUrls: ['./channel-listing.component.scss']
})
export class ChannelListingComponent implements OnInit {
  @ViewChild('sidebar') sidebar: FilterSidebarComponent
  constructor(private service: ChannelListingService, private route: ActivatedRoute) { }

  listing: any[];
  requestStatus: string = 'waiting';
  title: string
  id: number;
  filters: {from: Time, to: Time, date: number};

  updateFilters(filter: any){
    this.filters = filter;
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
      response => { this.title = response.name; }
    )
  }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.sidebar.filtersChanged
      .pipe(debounceTime(500))
      .subscribe(ev => this.updateFilters(ev));
  }
}
