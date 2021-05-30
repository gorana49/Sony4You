using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models
{
    public class Message
    {
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverUsername { get; set; }
        public string Text { get; set; }
    }
}
