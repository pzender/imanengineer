import { Component, OnInit } from '@angular/core';
import { NotificationsService } from './utilities/notifications.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'TV Guide';

  constructor(private notificationService: NotificationsService, 
    private http: HttpClient) {  }

  ngOnInit(): void {
    this.notificationService.initConnection();
    this.notificationService.addNotificationListener();
    //this.http.get()
  }

}
