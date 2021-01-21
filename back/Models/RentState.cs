using System;
using System.Collections.Generic;
using System.Linq;

namespace back.Models{
    public class RentState{
        public int Id { get; set; } 
        public string Address { get; set; }
        public DateTime StartTime { get; set; }     
        public DateTime EndTime { get; set; } 
        public DateTime OrderTime { get; set; }       
        public bool Approved { get; set; }
        public double Review { get; set; }
        public string Comment { get; set; }
        public bool Finished { get; set; }
       
    }

}