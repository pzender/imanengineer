import { Component, OnInit } from '@angular/core';
import { NotificationsService } from './shared/services/notifications.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Looking Glass';

  constructor(private notificationService: NotificationsService) {  }

  ngOnInit(): void {
    Notification.requestPermission().then(result => {
      console.log(`[Notification.requestPermission] ${result}`)
    })

    this.notificationService.initConnection();
    this.notificationService.addNotificationListener();
  }
}
