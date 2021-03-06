import { Component, OnInit, ViewChild } from '@angular/core';
import { NotificationsService } from './notifications.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';
import { Time } from '@angular/common';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
  @ViewChild('sidebar') sidebar: FilterSidebarComponent

  constructor(private service: NotificationsService, private route: ActivatedRoute, public user: UserService) { }

  listing: any[];
  requestStatus: string = 'waiting';
  title: string
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
