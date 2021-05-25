using System;

namespace back.DtoModels
{
    public class RentererContestDTO
    {
        public string UsernameRenterer { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NumOfPlayers { get; set; }
        public decimal Price { get; set; }
        public string Award { get; set; }
    }
}
