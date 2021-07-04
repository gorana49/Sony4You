using System;

namespace back.DtoModels
{
    public class MessageDTO
    {
        public string clientuniqueid { get; set; }
        public string senderUsername { get; set; }
        public string receiverId { get; set; }
        public string receiverUsername { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
    }
}
