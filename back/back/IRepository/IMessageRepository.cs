using back.DtoModels;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface IMessageRepository
    {
        Task SendMessage(MessageDTO message);
        Task<IEnumerable<MessageDTO>> ReceiveMessage(string senderUsername, string receiverUsername, string from, int count);
        Task StartConversation(ConversationDTO participants);
        Task SendNotification(NotificationDTO notiffication);
        Task<IEnumerable<NotificationDTO>> ReceiveNotiffication(string senderUsername, string receiverUsername, string from, int count);
        //Task SetTimeToLiveForStream(int senderId, int receiverId);
        //Task<int> GetTimeToLiveForStream(int senderId, int receiverId);
        Task<IEnumerable<Rentee>> GetRentererInChatWith(int rentererId);
        Task<IEnumerable<int>> GetIdsRentererInChatWith(int rentererId);
        Task<IEnumerable<Renterer>> GetRenteeInChatWith(int renteeId);
        Task<IEnumerable<int>> GetIdsRenteeInChatWith(int renteeId);
        Task DeleteConversation(int biggerId, int smallerId);
    }
}
