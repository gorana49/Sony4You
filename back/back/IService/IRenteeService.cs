using back.DtoModels;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back.IService
{
    public interface IRenteeService
    {
        public Task AddRentee(Rentee rentee);
        public Task<List<Rentee>> GetAllRentees();
        public Task<Rentee> GetRentee(string Username);
        public Task DeleteRentee(string Username);
        public Task UpdateRentee(UpdateRenteeDTO rentee);
        public Task AddParticipant(Contest _contest, Rentee renteee);
        public Task AddCommentToRenterer(Comment comm, string Username, string UsernameRentee);
        public Task DeleteComment(string title);
        public Task<List<Comment>> GetCommentRentee(string UsernameRentee, string UsernameRenterer);
        public Task UpdateComment(string Text, string Title);
        public Task<List<AvailableSonyDTO>> GetAvailableSonys();
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername);
        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername);
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername);
        public Task CancelReservation(ReservationPreviewDTO previe);
    }
}
