using System;
using System.ComponentModel.DataAnnotations;

namespace back.Models
{
    public class Renterer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}