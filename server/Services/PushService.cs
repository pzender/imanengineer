using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using TV_App.Data_Transfer_Objects;
using TV_App.Models;
using WebPush;


namespace TV_App.Services
{
    public class PushService
    {
        private const string PUBLIC_KEY = @"BJmJ27fKmbkzBH7XkeJWzmEdqtRM8S4SpV6btWP6IlwUwmNgiLCg63az0JYWbL7HR4ah6BVVhiyniAMAAaPfu3w";
        private const string PRIVATE_KEY = @"2dU0c_kb2Dk2Xn96JsaxVkvQyxSz_Ckxg9pQ9XtWOzA";
        private const string SUBJECT = @"https://www.lookingglass.gq";
        private readonly TvAppContext db = new TvAppContext();

        public void Notify(string username, string title, string body)
        {
            User u = db.Users
                .Include(u => u.Subscriptions)
                .Single(u => u.Login == username);
            Console.WriteLine($"[{DateTime.Now}] Notification - {u.Login}");

            foreach (Subscription s in u.Subscriptions)
            {
                var subscription = new PushSubscription(s.PushEndpoint, s.PushP256dh, s.PushAuth);
                var vapidDetails = new VapidDetails(SUBJECT, PUBLIC_KEY, PRIVATE_KEY);
                var payload = new MessageDTO(title, body);
                var webPushClient = new WebPushClient();
                Console.WriteLine($"[{DateTime.Now}] Sending to {s.PushEndpoint}");
                try
                {
                    webPushClient.SendNotification(subscription, JsonConvert.SerializeObject(payload), vapidDetails);
                }
                catch(WebPushException e)
                {
                    Console.WriteLine($"[{DateTime.Now}] Error - {e.Message}");
                }
            }
        }
    }
}
