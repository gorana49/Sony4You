﻿using back.DtoModels;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back
{
    public interface IRentererRepository
    {
        public Task AddRenterer(RentererDTO renterer);
        public Task<List<Renterer>> GetAllRenterers();
        public Task<Renterer> GetRenterer(string CompanyName);
        public Task DeleteRenterer(string Name);
        public Task UpdateRenterer(UpdateRentererDTO renter);
    }
}
