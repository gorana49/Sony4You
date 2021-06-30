using System;
using System.ComponentModel.DataAnnotations;

namespace back.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Players { get; set; }
        public int YearOfProduction { get; set; }
        public string Description {get;set;}
        public string Type {get;set;}

    }
}