import { Component, OnInit } from '@angular/core';
import { ChannelListingService } from './channel-listing.service';
import { ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';

@Component({
  selector: 'app-channel-listing',
  templateUrl: './channel-listing.component.html',
  styleUrls: ['./channel-listing.component.scss']
})
export class ChannelListingComponent implements OnInit {

  constructor(private service: ChannelListingService, private route: ActivatedRoute) { }

  listing: any[];
  requestStatus: string;
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
      response => { this.title = response.name; }
    )
  }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
  }

}
