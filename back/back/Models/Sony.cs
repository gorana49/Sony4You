using System;
using System.ComponentModel.DataAnnotations;

namespace back.Models
{
    public class Sony
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
    }
}