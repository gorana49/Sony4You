using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.DtoModels
{
    public class ChatMessageDTO
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime SendTime { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
    }
}
