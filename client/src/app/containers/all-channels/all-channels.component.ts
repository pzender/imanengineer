import { Component, OnInit } from '@angular/core';
import { ChannelLink } from 'src/app/interfaces/Channel';
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

  ngOnInit() {
    this.channelService.fetch().subscribe(
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
