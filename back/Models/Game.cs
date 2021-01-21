using System;
using System.Collections.Generic;
using System.Linq;

namespace back.Models{
    public class Game{
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Type { get; set; }     
        public int Year { get; set; } 
        public string Description { get; set; }
        public bool Multiplayer { get; set; }
       
    }

}