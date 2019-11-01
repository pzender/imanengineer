import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr'
import { from } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private hubConnection: HubConnection;
  constructor() { 

  }

  public initConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.api}notificationHub`)
      .build();
    this.hubConnection.start()
      .then(() => console.log('Connection started!'))
      .catch(error => console.log('Coś nie poszło :( ' + error));
  }

  public addNotificationListener() {
    this.hubConnection.on('notify', 
      notification => this.notify(notification))
  }

  public notify(message: string) {
    if (!("Notification" in window)) {
      console.log("This browser does not support system notifications");
    }
    const notification = new Notification('TV_App', { body: message} )
  }


}
