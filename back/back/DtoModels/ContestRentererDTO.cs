using System;

namespace back.DtoModels
{
    public class ContestRentererDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NumOfPlayers { get; set; }
        public decimal Price { get; set; }
        public string Award { get; set; }

        public string NameRent { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }

    }
}
