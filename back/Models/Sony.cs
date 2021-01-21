using System;
using System.Collections.Generic;
using System.Linq;

namespace back.Models
{
    public class Sony{
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public int Available { get; set; }
        public double Price { get; set; }
        public int ProductionYear { get; set; }
        
    }
}