using back.DtoModels;
using back.IRepository;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back
{
    public class RentererRepository : IRentererRepository
    {
        private readonly IBoltGraphClient _client;
        private readonly IRedisRepository _redisRepository;
        public RentererRepository(IBoltGraphClient client, IRedisRepository redisRepo)
        {
            _client = client;
            _client.Cypher.CreateUniqueConstraint("(renterer:Renterer)", "renterer.Id");
            _redisRepository = redisRepo;
        }
        public async Task<bool> AddRenterer(Renterer renterer)
        {
            var flag = this.IfRentererExists(renterer.Username).Result;
            if (flag == false)
            {
                var result =  _client.Cypher.Create("(renterer:Renterer {renterer})").WithParams(new { renterer }).Set("renterer.Id = id(renterer)").Return(renterer => new
                {
                    Renterer = renterer.As<Renterer>()
                }).ResultsAsync.IsCompletedSuccessfully;
                if (result) {
                    return true;
                    LoggedUserDTO user = new LoggedUserDTO(renterer.Id, renterer.Name, renterer.Password, true, "renterer");
                    await _redisRepository.AddNewLoggedUser(user);
                }
                return false;
            }
            return false;
        }
        public async Task<bool> IfRentererExists(string username)
        {
            var result = await _client.Cypher
                .Match("(renterer:Renterer)")
                .Where((Renterer renterer) => renterer.Username == username)
                .Return<int>("count(renterer)")
                .ResultsAsync;
            return result.Single() > 0;
        }
        public async Task<List<Renterer>> GetAllRenterers()
        {
            var result = await _client.Cypher.Match(@"(renterer:Renterer)").Return(renterer => new
            {
                Renterer = renterer.As<Renterer>()
            }).Limit(10).ResultsAsync;
            List<Renterer> list = new List<Renterer>();
            foreach (var indeks in result)
            {
                list.Add(indeks.Renterer);
            }
            return list;
        }
        public async Task<Renterer> GetRenterer(string username)
        {
            Renterer rent = new Renterer();
            var result = await _client.Cypher.Match(@"(renterer:Renterer)")
                .Where((Renterer renterer) => renterer.Username == username)
                .Return(renterer => new { Renterer = renterer.As<Renterer>() }).Limit(1).ResultsAsync;
            foreach (var indeks in result)
            {
                rent = indeks.Renterer;
            }
            return rent;
        }
        public Task DeleteRenterer(string Name)
        {
            var result = _client.Cypher.Match("(renterer:Renterer)")
                .Where((Renterer renterer) => renterer.Username == Name)
                .DetachDelete("renterer")
                .ExecuteWithoutResultsAsync();
            return result;
        }

        public Task UpdateRenterer(UpdateRentererDTO renter)
        {
            var result = _client.Cypher.Match(@"(renterer:Renterer)").Where((Renterer renterer) => renterer.Name == renter.Name)
                .Set("renterer.Password = " + "\"" + renter.Password + "\"").Set("renterer.PhoneNumber = " + "\"" + renter.PhoneNumber + "\"")
                .ExecuteWithoutResultsAsync();
            return result;
        }
    }
}
