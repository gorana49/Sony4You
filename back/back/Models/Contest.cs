using System;

namespace back.Models
{
    public class Contest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NumOfPlayers { get; set; }
        public decimal Price { get; set; }
        public string Award { get; set; }
        public string Winner { get; set; }
        public Contest(string Na, DateTime Dt, int Num, decimal Pr, string Award)
        {
            Name = Na;
            Date = Dt;
            NumOfPlayers = Num;
            Price = Pr;
            this.Award = Award;
        }
        public Contest() { }
    }
}
