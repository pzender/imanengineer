using TV_App.Models;

namespace TV_App.DataTransferObjects
{
    public class ChannelDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public ChannelDTO(Channel channel)
        {
            Id = channel.Id;
            Name = channel.Name;
            Icon = channel.IconUrl;
        }
    }
}