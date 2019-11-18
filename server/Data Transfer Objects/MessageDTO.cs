using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.DataTransferObjects
{
    public class MessageDTO
    {
        public MessageContent notification { get; set; }
        public MessageDTO(string title, string body)
        {
            this.notification = new MessageContent()
            {
                title = title,
                body = body
            };
        }
    }

    public class MessageContent
    {
        public string title { get; set; }
        public string body { get; set; }
    }

}
