using System;
using System.Collections.Generic;
using System.Linq;

namespace back.Models
{
    public class GenerateId{
        public int RenterId { get; set; }
        public int RenteeId { get; set; }
        public int JoystickId { get; set; }
        public int GameId { get; set; }
        public int RentStateId { get; set; }
    }
}