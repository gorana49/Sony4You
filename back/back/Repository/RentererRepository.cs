using back.DtoModels;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back
{
    public class RentererRepository : IRentererRepository
    {
        private readonly IBoltGraphClient _client;
        public RentererRepository(IBoltGraphClient client)
        {
            _client = client;
        }
        public async Task AddRenterer(Renterer renterer)
        {
            await _client.Cypher.Create("(renterer:Renterer {renterer})").WithParams(new { renterer }).ExecuteWithoutResultsAsync();
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
        public async Task<Renterer> GetRenterer(string CompanyName)
        {
            Renterer rent = new Renterer();
            var result = await _client.Cypher.Match(@"(renterer:Renterer)")
                .Where((Renterer renterer) => renterer.CompanyName == CompanyName)
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
                .Where((Renterer renterer) => renterer.Name == Name)
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
