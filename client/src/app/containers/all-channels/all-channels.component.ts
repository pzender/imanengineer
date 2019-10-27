import { Component, OnInit } from '@angular/core';
import { ChannelLink } from 'src/app/shared/interfaces/Channel';
import { AllChannelsService } from './all-channels.service';

@Component({
  selector: 'app-all-channels',
  templateUrl: './all-channels.component.html',
  styleUrls: ['./all-channels.component.scss']
})
export class AllChannelsComponent implements OnInit {

  constructor(private channelService: AllChannelsService) { }
  public requestStatus = "none";
  public channels: ChannelLink[] = [];

  updateFilters(filter: any) {
    this.fetch(filter);
  }

  ngOnInit() {
  }

  fetch(filter: any){
    this.requestStatus = "waiting";
    this.channelService.fetch(filter).subscribe(
      response => { 
        this.channels = response; 
        this.requestStatus = response.length > 0 ? "success" : "empty";
      },
      error => {
        this.requestStatus = "failure";
      }
    )
  }
}
