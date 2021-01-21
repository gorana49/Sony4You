using System;
using System.Collections.Generic;
using System.Linq;

namespace back.Models
{
    public class Rentee{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string  Email { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public string IDNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}