import { Component, OnInit } from '@angular/core';
import { ChannelListingService } from './channel-listing.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-channel-listing',
  templateUrl: './channel-listing.component.html',
  styleUrls: ['./channel-listing.component.scss']
})
export class ChannelListingComponent implements OnInit {

  constructor(private service: ChannelListingService, private route: ActivatedRoute) { }

  listing: any[];
  error: {status: number, message: string};
  title: string
  id: number;
  

  filters($event){
    console.log($event);
  }

  fetch(){
    this.error = {status: undefined, message:'Szukamy!'};
    this.service.fetch(this.id).subscribe(
      response => { 
        this.listing = response; 
        this.error = {status: 200, message: 'OK'};
      },
      error => {
        //this.listing = [];
        this.error = {status: error.status, message: error.statusText};
      }
    )
    this.service.info(this.id).subscribe(
      response => { this.title = response.name; }
    )
  }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.fetch()
  }

}
