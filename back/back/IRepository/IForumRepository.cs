using back.DtoModels;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface IForumRepository
    {
        public Task SendMessage(Message message);
        public Task<IEnumerable<MessageDTO>> ReceiveMessage(int senderId, int receiverId, string from, int count);
    }
}
