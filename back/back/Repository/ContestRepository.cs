using back.IRepository;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back.Repository
{
    public class ContestRepository : IContestRepository
    {
        private readonly IBoltGraphClient _client;
        public ContestRepository(IBoltGraphClient client)
        {
            _client = client;
        }
        public Task AddContest(Contest contest, Renterer renter)
        {
            var result = _client.Cypher.Create("(contest:Contest {contest})").WithParams(new { contest })
                .Match(@"(renterer:Renterer)").Where((Renterer renterer) => renterer.Name == renter.Name)
                .Create("(renterer) -[r:ORGANISE]-> (contest)").ExecuteWithoutResultsAsync();
            return result;
        }

        public Task AddParticipant(Contest _contest, Rentee renteee)
        {
            var result = _client.Cypher.Match(@"(rentee:Rentee)").Where((Rentee rentee) => rentee.Name == renteee.Name)
                .Match(@"contest:Contest").Where((Contest contest) => contest.Date == _contest.Date)
                .Create("(contest) - [r:PARTICIPATED_IN] -> (rentee)").ExecuteWithoutResultsAsync();
            return result;
        }

        public Task UpdateContest(string Username, Contest cont)
        {
            var result = _client.Cypher.Match(@"(contest:Contest)").Where((Contest contest) => contest.Name == cont.Name)
                .Set("contest.Winner" + "\"" + Username + "\"").ExecuteWithoutResultsAsync();
            return result;
        }

        public Task DeleteContest(string Name)
        {
            var result = _client.Cypher.Match("(contest:Contest)")
               .Where((Contest contest) => contest.Name == Name)
               .DetachDelete("contest")
               .ExecuteWithoutResultsAsync();
            return result;
        }

        public async Task<List<Contest>> GetAllContest()
        {
            var result = await _client.Cypher.Match(@"(contest:Contest)").Return(contest => new
            {
                Contest = contest.As<Contest>()
            }).Limit(10).ResultsAsync;
            List<Contest> list = new List<Contest>();
            foreach (var indeks in result)
            {
                list.Add(indeks.Contest);
            }
            return list;
        }

        public async Task<Contest> GetContest(string Name)
        {
            Contest cont = new Contest();
            var result = await _client.Cypher.Match(@"contest:Contest").Where((Contest contest) => contest.Name == Name)
                .Return((contest) => new
                {
                    Contest = contest.As<Contest>()
                }).Limit(1).ResultsAsync;
            foreach (var indeks in result)
            {
                cont = indeks.Contest;
            }
            return cont;
        }
    }
}
