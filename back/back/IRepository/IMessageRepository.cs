using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.Models;
using back.DtoModels;

namespace back.IRepository
{
    public interface IMessageRepository
    {
        Task SendMessage(Message message);
        Task<IEnumerable<MessageDTO>> ReceiveMessage(int senderId, int receiverId, string from, int count);
        Task StartConversation(ConversationDTO participants);
        //Task SetTimeToLiveForStream(int senderId, int receiverId);
        //Task<int> GetTimeToLiveForStream(int senderId, int receiverId);
        Task<IEnumerable<Rentee>> GetRentererInChatWith(int rentererId);
        Task<IEnumerable<int>> GetIdsRentererInChatWith(int rentererId);
        Task<IEnumerable<Renterer>> GetRenteeInChatWith(int renteeId);
        Task<IEnumerable<int>> GetIdsRenteeInChatWith(int renteeId);
        Task DeleteConversation(int biggerId, int smallerId);
    }
}
