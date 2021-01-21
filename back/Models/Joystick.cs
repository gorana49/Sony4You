using System;
using System.Collections.Generic;
using System.Linq;

namespace back.Models
{
    public class Joystick{
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
    }
}