using System;
using System.ComponentModel.DataAnnotations;

namespace back.Models
{
    public class Rentee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public decimal Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }

    }
}