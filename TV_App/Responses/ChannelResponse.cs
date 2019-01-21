using TV_App.EFModels;

namespace TV_App.Responses
{
    public class ChannelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ChannelResponse(Channel channel)
        {
            Id = (int)channel?.Id;
            Name = channel.Name;
        }
    }
}