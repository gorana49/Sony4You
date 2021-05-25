﻿using back.DtoModels;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back.IRepository
{
    public interface IRenteeRepository
    {
        public Task AddRentee(Rentee rentee);
        public Task<List<Rentee>> GetAllRentees();
        public Task<Rentee> GetRentee(string Username);
        public Task DeleteRentee(string Username);
        public Task UpdateRentee(UpdateRenteeDTO rentee);
    }
}