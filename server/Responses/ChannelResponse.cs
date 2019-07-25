using TV_App.Models;

namespace TV_App.Responses
{
    public class ChannelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public ChannelResponse(Channel channel)
        {
            Id = (int)channel?.Id;
            Name = channel.Name;
            Icon = channel.IconUrl;
        }
    }
}