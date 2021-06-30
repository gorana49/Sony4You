using back.DtoModels;
using back.IRepository;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Repository
{
    public class RenteeRepository : IRenteeRepository
    {
        private readonly IBoltGraphClient _client;
        private readonly IRedisRepository _redisRepository;
        public RenteeRepository(IBoltGraphClient client, IRedisRepository redisRepository)
        {
            _client = client;
            _client.Cypher.CreateUniqueConstraint("(rentee:Rentee)", "rentee.Id");
            _redisRepository = redisRepository;
        }
        public async Task AddRentee(Rentee rentee)
        {
            var flag = this.IfRenteeExists(rentee.Username).Result;
            if (flag == false)
            {
                var result = await _client.Cypher.Create("(rentee:Rentee {rentee})").WithParams(new { rentee }).Return(rentee => new
                {
                    Rentee = rentee.As<Rentee>()
                }).ResultsAsync;
                LoggedUserDTO user = new LoggedUserDTO(result.First().Rentee.Id, result.First().Rentee.Username, result.First().Rentee.Password.ToString(), true, "rentee");
                await _redisRepository.AddNewLoggedUser(user);
            }
        }
        public async Task<bool> IfRenteeExists(string username)
        {
            var result = await _client.Cypher
                .Match("(renterer:Renterer)")
                .Where((Renterer renterer) => renterer.Username == username)
                .Return<int>("count(renterer)")
                .ResultsAsync;
            return result.Single() > 0;
        }
        public async Task<List<Rentee>> GetAllRentees()
        {
            var result = await _client.Cypher.Match(@"(rentee:Rentee)").Return(rentee => new
            {
                Rentee = rentee.As<Rentee>()
            }).Limit(10).ResultsAsync;
            List<Rentee> list = new List<Rentee>();
            foreach (var indeks in result)
            {
                list.Add(indeks.Rentee);
            }
            return list;
        }
        public async Task<Rentee> GetRentee(string Username)
        {
            Rentee rent = new Rentee();
            var result = await _client.Cypher.Match(@"(rentee:Rentee)")
                .Where((Rentee rentee) => rentee.Username == Username)
                .Return(rentee => new { Rentee = rentee.As<Rentee>() }).Limit(1).ResultsAsync;
            foreach (var indeks in result)
            {
                rent = indeks.Rentee;
            }
            return rent;
        }
        public Task DeleteRentee(string Username)
        {
            var result = _client.Cypher.Match("(rentee:Rentee)")
                .Where((Rentee rentee) => rentee.Username == Username)
                .DetachDelete("rentee")
                .ExecuteWithoutResultsAsync();
            return result;
        }

        public Task UpdateRentee(UpdateRenteeDTO renteee)
        {
            var result = _client.Cypher.Match(@"(rentee:Rentee)").Where((Rentee rentee) => rentee.Name == renteee.Name)
                .Set("rentee.Password = " + "\"" + renteee.Password + "\"").Set("rentee.PhoneNumber = " + "\"" + renteee.PhoneNumber + "\"")
                .ExecuteWithoutResultsAsync();
            return result;
        }
    }
}
