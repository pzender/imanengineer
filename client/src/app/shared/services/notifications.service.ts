import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { UserService } from './user.service';
import { SwPush } from '@angular/service-worker';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  readonly VAPID_PUBLIC_KEY = "BJmJ27fKmbkzBH7XkeJWzmEdqtRM8S4SpV6btWP6IlwUwmNgiLCg63az0JYWbL7HR4ah6BVVhiyniAMAAaPfu3w";
  static SUBSCRIPTION_ID = 'subscription_id';
  constructor(
    private http: HttpClient, 
    private swPush: SwPush
  ) { }

  public subscribeToNotifications(username: string) {
    this.swPush.requestSubscription({
      serverPublicKey: this.VAPID_PUBLIC_KEY
    })
    .then(sub => this.addPushSubscriber(sub, username))
    .catch(err => console.error("Could not subscribe to notifications", err));
  }

  public unsubscribeToNotifications(username: string) {
    this.swPush.unsubscribe().then(console.log);
    
    const sub_id = localStorage.getItem(NotificationsService.SUBSCRIPTION_ID);
    this.http.delete(`${environment.api}Users/${username}/Subscribtions/${sub_id}`).subscribe(console.log)
    localStorage.removeItem(NotificationsService.SUBSCRIPTION_ID);
  }
  
  private addPushSubscriber(sub:any, username: string) {
    this.http.post(`${environment.api}Users/${username}/Subscribtions`, sub).subscribe(
      resp => {
        localStorage.setItem(NotificationsService.SUBSCRIPTION_ID, resp['id'])
      }
    );
    
  }
}
