using System.Threading.Tasks;

namespace back.IRepository
{
    public interface IFriendRequestRepository
    {
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername);
        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername);
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername);

    }
}
