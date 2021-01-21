using System;
using System.Collections.Generic;
using System.Linq;

namespace back.Models{
    public class Renter{
        public int Id { get; set; } 
        public string  Name { get; set; }   
        public string Address { get; set; }
        public string CompanyName { get; set; }     
        public string Username {  get; set; }
        public string Email { get; set; }
        public string  Password { get; set; }
        public int MobileNumber { get; set; }   
        public int NumberOfReviews { get; set; }
        public double Review { get; set; }
        public string ImageIUrl { get; set; }
        
    }

}