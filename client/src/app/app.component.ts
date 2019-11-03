import { Component, OnInit } from '@angular/core';
import { NotificationsService } from './shared/services/notifications.service';
import { SwPush } from '@angular/service-worker';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Looking Glass';
  constructor() {  }

  ngOnInit(): void {
    
  }
}
