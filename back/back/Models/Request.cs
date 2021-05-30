using System;

namespace back.Models
{
    public class Request
    {
        public string SenderRequestUsername { get; set; }
        public string ReceiverRequestUsername { get; set; }
        public DateTime Date { get; set; }
    }
}
