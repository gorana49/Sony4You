using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.Models;

namespace back.DtoModels
{
    public class ConversationDTO
    {
        public Rentee Sender { get; set; }
        public Renterer Receiver { get; set; }
        public string Text { get; set; }
    }
}
