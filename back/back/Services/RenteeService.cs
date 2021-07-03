using back.DtoModels;
using back.IRepository;
using back.IService;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.Services
{
    public class RenteeService : IRenteeService
    {
        private readonly IRenteeRepository _renteeRepository;
        private readonly IContestRepository _contestRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISonyRepository _sonyRepository;
        private readonly IFriendRequestRepository _friendRepository;
        public RenteeService(IRenteeRepository renteeRep, IContestRepository contestRepo, ICommentRepository commRepo, ISonyRepository sonyRepo, IFriendRequestRepository friendRepo)
        {
            _renteeRepository = renteeRep;
            _contestRepository = contestRepo;
            _commentRepository = commRepo;
            _sonyRepository = sonyRepo;
            _friendRepository = friendRepo;
        }

        public async Task<RenteeDTO> AddRentee(RenteeDTO rentee)
        {
            return await this._renteeRepository.AddRentee(rentee);
        }
        public async Task<List<Rentee>> GetAllRentees()
        {
            return await this._renteeRepository.GetAllRentees();
        }
        public async Task<Rentee> GetRentee(string Username)
        {
            return await this._renteeRepository.GetRentee(Username);
        }
        public Task DeleteRentee(string Username)
        {
            return this._renteeRepository.DeleteRentee(Username);
        }
        public Task<Rentee> UpdateRentee(UpdateRenteeDTO rentee)
        {
            return this._renteeRepository.UpdateRentee(rentee);
        }
        public Task AddParticipant(Contest _contest, Rentee renteee)
        {
            return this._contestRepository.AddParticipant(_contest, renteee);
        }
        public Task AddCommentToRenterer(Comment comm, string Username, string UsernameRentee)
        {
            return this._commentRepository.AddCommentToRenterer(comm, Username, UsernameRentee);
        }
        public Task DeleteComment(string title)
        {
            return this._commentRepository.DeleteComment(title);
        }
        public Task<List<Comment>> GetCommentRentee(string UsernameRentee, string UsernameRenterer)
        {
            return this._commentRepository.GetCommentRentee(UsernameRentee, UsernameRenterer);
        }
        public Task UpdateComment(string Text, string Title)
        {
            return this._commentRepository.UpdateComment(Text, Title);
        }
        public Task<List<AvailableSonyDTO>> GetAvailableSonys()
        {
            return this._sonyRepository.GetAvailableSonys();
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
