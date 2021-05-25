using back.DtoModels;
using back.IRepository;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.Repository
{
    public class RenteeRepository : IRenteeRepository
    {
        private readonly IBoltGraphClient _client;
        public RenteeRepository(IBoltGraphClient client)
        {
            _client = client;
        }
        public async Task AddRentee(Rentee rentee)
        {
            await _client.Cypher.Create("(rentee:Rentee {rentee})").WithParams(new { rentee }).ExecuteWithoutResultsAsync();
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
