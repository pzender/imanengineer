using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TV_App.Models;
using Microsoft.AspNetCore.SignalR;
using TV_App.Services;
using WebPush;
using Microsoft.EntityFrameworkCore;
using TV_App.DataTransferObjects;
using System.Collections.Generic;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController : ControllerBase
    {
        private readonly TvAppContext db = new TvAppContext();
        private readonly ProgrammeService service = new ProgrammeService();
        private readonly PushService push = new PushService();

        // GET: api/Util
        [HttpGet]
        public string Get()
        {
            return $"{DateTime.Now} , {DateTime.UtcNow}";
        }

        //POST: api/Util/Przemek
        [HttpPost("{name}")]
        public void Notify(string name)
        {
            User user = db.Users.Include(u => u.Subscriptions).Single(u => u.Login == name);
            push.Notify(user, "test", "test");
        }

        //POST: api/Util/SendNotifications
        [HttpPost("SendNotifications")]
        public List<ProgrammeDTO> SendNotifications()
        {
            var users = db.Users.Include(u => u.Subscriptions);
            List<ProgrammeDTO> notificationsToSend = new List<ProgrammeDTO>();
            foreach(User user in users)
            {
                foreach(ProgrammeDTO notification in service.GetNotificationsFor(user.Login))
                {
                    string sending = "sending";
                    string too_soon = "too soon";
                    Console.WriteLine($"[{DateTime.Now}] {notification.Title} at {notification.Emissions.First().Start} - {(notification.Emissions.First().Start < DateTime.Now.AddMinutes(30) ? sending : too_soon)}");
                    
                    if (notification.Emissions.First().Start < DateTime.Now.AddMinutes(30) )
                    {
                        notificationsToSend.Add(notification);
                        var time_diff = notification.Emissions.First().Start - DateTime.Now;
                        if (time_diff > TimeSpan.Zero)
                        {
                            push.Notify(user, 
                                $"Twój program zaczyna się za {Math.Floor(time_diff.TotalMinutes)} minut", 
                                $"{notification.Title} na {notification.Emissions.First().Channel.Name}: {notification.Emissions.First().Start.TimeOfDay}"
                            );
                        }
                        var notificationEntity = db.Notifications.First(not => not.EmissionId == notification.Emissions.First().Id && not.UserLogin == user.Login);
                        db.Notifications.Remove(notificationEntity);
                    }
                }
            }
            db.SaveChanges();
            return notificationsToSend;
        }
    }
}
