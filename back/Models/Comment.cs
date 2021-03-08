using System;
using System.ComponentModel.DataAnnotations;

namespace back.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text{ get; set; }

    }
}