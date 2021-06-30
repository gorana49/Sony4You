using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface IContestRepository
    {
        public Task AddContest(Contest contest, Renterer renter);
        public Task AddParticipant(Contest _contest, Rentee renteee);
        public Task UpdateContest(string Username, Contest cont);
        public Task DeleteContest(string Name);
        public Task<List<Contest>> GetAllContest();
        public Task<Contest> GetContest(string Name);
    }
}
