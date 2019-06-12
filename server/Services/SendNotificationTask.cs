using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TV_App.Scheduler;

namespace TV_App.Services
{
    public class SendNotificationTask : IScheduledTask
    {
        IHubContext<NotificationHub> hubContext;

        public SendNotificationTask(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public string Schedule => "* * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await hubContext.Clients.All.SendAsync("notify", "Scheduled notification at " + DateTime.Now.ToShortTimeString());
        }
    }
}
