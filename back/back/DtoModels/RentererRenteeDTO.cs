using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.Models;

namespace back.DtoModels
{
    public class RentererRenteeDTO
    {
        public int Id { get; set; }
        public Renterer Renterer { get; set; }
        public Rentee Rentee { get; set; }
    }
}
