using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface IGameRepository
    {
        public Task AddGame(string SerialNumber, Game game);
        public Task UpdateGame(string Players, Game gam);
        public Task DeleteGame(string Name);
        public Task<List<Game>> GetGamesOnSony(string SerialNumber);

    }
}
