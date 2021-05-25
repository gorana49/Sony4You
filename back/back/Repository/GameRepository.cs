using back.IRepository;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly IBoltGraphClient _client;
        public GameRepository(IBoltGraphClient client)
        {
            _client = client;
        }
        public Task AddGame(string SerialNumber, Game game)
        {
            var result = _client.Cypher.Create("(game:Game {game})").WithParams(new { game })
                .Match(@"(sony:Sony)").Where((Sony sony) => sony.SerialNumber == SerialNumber)
                .Create("(sony) -[r:HAS]-> (game)").ExecuteWithoutResultsAsync();
            return result;
        }

        public Task UpdateGame(string Players, Game gam)
        {
            var result = _client.Cypher.Match(@"(game:Game)").Where((Game game) => game.Name == gam.Name)
                .Set("game.Players" + "\"" + Players + "\"").ExecuteWithoutResultsAsync();
            return result;
        }

        public Task DeleteGame(string Name)
        {
            var result = _client.Cypher.Match("(game:Game)")
               .Where((Game game) => game.Name == Name)
               .DetachDelete("game")
               .ExecuteWithoutResultsAsync();
            return result;
        }

        //public Task<List<Game>> GetAllGames(string Username)
        //{
        //    var result = _client.Cypher.Match("renterer:Renterer").Where((Renterer renterer) => renterer.Username == Username)
        //        .AndWhere((Sony sony) => )
        //}
        public async Task<List<Game>> GetGamesOnSony(string SerialNumber)
        {
            var result = await _client.Cypher.Match(@"(sony:Sony)-[r:HAS]->(game:Game)")
                .Where((Sony sony) => sony.SerialNumber == SerialNumber)
                .Return(game => new
                {
                    Game = game.As<Game>()
                }).Limit(10).ResultsAsync;

            List<Game> list = new List<Game>();
            foreach (var indeks in result)
            {
                list.Add(indeks.Game);
            }
            return list;
        }

    }
}
