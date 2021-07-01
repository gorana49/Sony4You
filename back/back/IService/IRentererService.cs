using back.DtoModels;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back
{
    public interface IRentererService
    {
        public Task<bool> AddRenterer(Renterer renterer);
        public Task<List<Renterer>> GetAllRenterers();
        public Task<Renterer> GetRenterer(string CompanyName);
        public Task DeleteRenterer(string Name);
        public Task UpdateRenterer(UpdateRentererDTO renter);
        public Task AddSony(string Username, Sony sony);
        public Task DeleteSony(string SerialNumber);
        public Task UpdateSony(string Price, string SerialNumber);
        public Task<List<Sony>> GetMySonys(string Username);
        public Task<Sony> GetSonyBySerialNumber(string SerialNumber);
        public Task AddContest(Contest contest, Renterer renter);
        public Task UpdateContest(string Username, Contest cont);
        public Task DeleteContest(string Name);
        public Task<List<Contest>> GetAllContest();
        public Task<Contest> GetContest(string Name);
        public Task AddGame(string SerialNumber, Game game);
        public Task UpdateGame(string Players, Game gam);
        public Task DeleteGame(string Name);
        public Task<List<Game>> GetGamesOnSony(string SerialNumber);
        public Task<Sony> ReservedMySony(string SerialNumber, string UsernameRentee, RenterListDTO renterList);
        public Task<List<ReservationPreviewDTO>> GetReservedSonys(string UsernameRenterer);
        public Task CancelReservation(ReservationPreviewDTO previe);
        public Task AddCommentToRentee(Comment comm, string Username, string UsernameRenterer);
        public Task DeleteComment(string title);
        public Task UpdateComment(string Text, string Title);
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername);
        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername);
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername);

    }
}
