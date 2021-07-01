using back.DtoModels;
using back.IRepository;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back
{
    public class RentererService : IRentererService
    {
        private readonly IRentererRepository _rentererRepository;
        private readonly ISonyRepository _sonyRepository;
        private readonly IContestRepository _contestRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IFriendRequestRepository _friendRepository;
        public RentererService(IRentererRepository rentRepo, ISonyRepository sonyRepo, IContestRepository contRepo, IGameRepository gameRepo, ICommentRepository commRepo, IFriendRequestRepository friendRepo)
        {
            _rentererRepository = rentRepo;
            _sonyRepository = sonyRepo;
            _contestRepository = contRepo;
            _gameRepository = gameRepo;
            _commentRepository = commRepo;
            _friendRepository = friendRepo;
        }

        public async Task<bool> AddRenterer(Renterer renterer)
        {
           return await this._rentererRepository.AddRenterer(renterer);
        }
        public async Task<List<Renterer>> GetAllRenterers()
        {
            return await this._rentererRepository.GetAllRenterers();
        }
        public async Task<Renterer> GetRenterer(string username)
        {
            return await this._rentererRepository.GetRenterer(username);
        }
        public Task DeleteRenterer(string Name)
        {
            return this._rentererRepository.DeleteRenterer(Name);
        }
        public Task UpdateRenterer(UpdateRentererDTO renter)
        {
            return this._rentererRepository.UpdateRenterer(renter);
        }
        public Task AddSony(string Username, Sony sony)
        {
            return this._sonyRepository.AddSony(Username, sony);
        }
        public Task DeleteSony(string SerialNumber)
        {
            return this._sonyRepository.DeleteSony(SerialNumber);
        }
        public Task UpdateSony(string Price, string SerialNumber)
        {
            return this._sonyRepository.UpdateSony(Price, SerialNumber);
        }
        public Task<List<Sony>> GetMySonys(string Username)
        {
            return this._sonyRepository.GetMySonys(Username);
        }
        public Task<Sony> GetSonyBySerialNumber(string SerialNumber)
        {
            return this._sonyRepository.GetSonyBySerialNumber(SerialNumber);
        }
        public Task AddContest(Contest contest, Renterer renter)
        {
            return this._contestRepository.AddContest(contest, renter);
        }

        public Task AddParticipant(Contest _contest, Rentee renteee)
        {
            return this._contestRepository.AddParticipant(_contest, renteee);
        }
        public Task UpdateContest(string Username, Contest cont)
        {
            return this._contestRepository.UpdateContest(Username, cont);
        }
        public Task DeleteContest(string Name)
        {
            return this._contestRepository.DeleteContest(Name);
        }
        public Task<List<Contest>> GetAllContest()
        {
            return this._contestRepository.GetAllContest();
        }
        public Task<Contest> GetContest(string Name)
        {
            return this._contestRepository.GetContest(Name);
        }
        public Task AddGame(string SerialNumber, Game game)
        {
            return this._gameRepository.AddGame(SerialNumber, game);
        }
        public Task UpdateGame(string Players, Game gam)
        {
            return this._gameRepository.UpdateGame(Players, gam);
        }
        public Task DeleteGame(string Name)
        {
            return this._gameRepository.DeleteGame(Name);
        }
        public Task<List<Game>> GetGamesOnSony(string SerialNumber)
        {
            return this._gameRepository.GetGamesOnSony(SerialNumber);
        }

        public Task AddCommentToRentee(Comment comm, string Username, string UsernameRenterer)
        {
            return this._commentRepository.AddCommentToRentee(comm, Username, UsernameRenterer);
        }
        public Task DeleteComment(string title)
        {
            return this._commentRepository.DeleteComment(title);
        }
        public Task UpdateComment(string Text, string Title)
        {
            return this._commentRepository.UpdateComment(Text, Title);
        }
        public Task<Sony> ReservedMySony(string SerialNumber, string UsernameRentee, RenterListDTO renterList)
        {
            return this._sonyRepository.ReservedMySony(SerialNumber, UsernameRentee, renterList);
        }
        public Task<List<ReservationPreviewDTO>> GetReservedSonys(string UsernameRenterer)
        {
            return this._sonyRepository.GetReservedSonys(UsernameRenterer);
        }
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            return this._friendRepository.AddRequest(SenderRequestUsername, ReceiverRequestUsername);
        }
        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            return this._friendRepository.DeleteRequest(SenderRequestUsername, ReceiverRequestUsername);
        }
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername)
        {
            return this._friendRepository.MakeUsFriends(SenderUsername, ReceiverUsername);
        }
        public Task CancelReservation(ReservationPreviewDTO previe)
        {
            return this._sonyRepository.CancelReservation(previe);
        }
    }
}
