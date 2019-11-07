using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.DataTransferObjects;
using TV_App.Models;

namespace TV_App.Services
{
    public class ChannelService
    {
        private readonly TvAppContext db = new TvAppContext();
        public ChannelDTO GetSingle(int id)
        {
            var channel = db.Channels
                .Include(ch => ch.OfferedChannels)
                .Where(ch => ch.Id == id)
                .AsNoTracking()
                .SingleOrDefault();

            return new ChannelDTO(channel);
        }

        public IEnumerable<ChannelDTO> GetGroup(long offer_id = 0, long theme_id = 0)
        {
            var channels = db.Channels
                .Include(ch => ch.OfferedChannels)
                .AsNoTracking()
                .ToList();

            return from channel in channels
                   where 
                        channel.OfferedChannels.Any(oc => oc.OfferId == offer_id || offer_id == 0) &&
                        channel.OfferedChannels.Any(oc => oc.OfferId == theme_id || theme_id == 0)
                   select new ChannelDTO(channel);
        }

    }
}
