using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.IRepository;
using StackExchange.Redis;
using back.Models;
using back.DtoModels;
using System.Text.Json;

namespace back.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;
        //private readonly IHubContext<MessageHub> _hub;
        public MessageRepository(IRedisConnectionBuilder builder)
        {
            _redisConnection = builder.Connection;
        }
        public async Task SendMessage(Message message)
        {
            var values = new NameValueEntry[]
            {
                new NameValueEntry("sender_username", message.SenderUsername),
                new NameValueEntry("senderId", message.SenderId),
                new NameValueEntry("receiver_username", message.ReceiverUsername),
                new NameValueEntry("receiverId", message.ReceiverId),
                new NameValueEntry("text", message.Text)
            };
            IDatabase redisDB = _redisConnection.GetDatabase();
            int fromId = message.SenderId > message.ReceiverId ? message.SenderId : message.ReceiverId;
            int toId = message.SenderId < message.ReceiverId ? message.SenderId : message.ReceiverId;
            await redisDB.StreamAddAsync($"messages:{fromId}:{toId}:chat", values);

            var jsonMessage = JsonSerializer.Serialize(message);
            ISubscriber chatPubSub = _redisConnection.GetSubscriber();
            await chatPubSub.PublishAsync("chat.messages", jsonMessage);
        }

        public async Task<IEnumerable<MessageDTO>> ReceiveMessage(int senderId, int receiverId, string from, int count)
        {
            List<MessageDTO> retMessages = new List<MessageDTO>();
            int fromId = senderId > receiverId ? senderId : receiverId;
            int toId = senderId < receiverId ? senderId : receiverId;
            string channelName = $"messages:{fromId}:{toId}:chat";
            IDatabase redisDb = _redisConnection.GetDatabase();
            from = Uri.UnescapeDataString(from);
            var messages = await redisDb.StreamRangeAsync(channelName, minId: "-", maxId: from, count: count, messageOrder: Order.Descending);
            foreach (var message in messages)
            {
                MessageDTO mess = new MessageDTO
                {
                    Id = message.Id,
                    SenderUsername = message.Values.FirstOrDefault(value => value.Name == "sender").Value,
                    SenderId = int.Parse(message.Values.FirstOrDefault(value => value.Name == "senderId").Value),
                    ReceiverUsername = message.Values.FirstOrDefault(value => value.Name == "receiver").Value,
                    ReceiverId = int.Parse(message.Values.FirstOrDefault(value => value.Name == "receiverId").Value),
                    Text = message.Values.FirstOrDefault(value => value.Name == "content").Value
                };
                retMessages.Add(mess);
            }
            return retMessages;
        }

        public async Task StartConversation(ConversationDTO participants)
        {
            string senderSetKey = $"rentee:{participants.Sender.Id}:chats";
            string receiverSetKey = $"renterer:{participants.Receiver.Id}:chats";
            Rentee sender = participants.Sender;
            Renterer receiver = participants.Receiver;

            var senderSetValue = JsonSerializer.Serialize(participants.Receiver);
            var receiverSetValue = JsonSerializer.Serialize(participants.Sender);

            double score = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            SortedSetEntry[] senderSetEntry = new SortedSetEntry[] { new SortedSetEntry(senderSetValue, score) };
            SortedSetEntry[] receiverSetEntry = new SortedSetEntry[] { new SortedSetEntry(receiverSetValue, score) };

            IDatabase redisDB = _redisConnection.GetDatabase();
            await redisDB.SortedSetAddAsync(senderSetKey, senderSetEntry);
            await redisDB.SortedSetAddAsync(receiverSetKey, receiverSetEntry);
        }

        //public async Task SetTimeToLiveForStream(int senderId, int receiverId)
        //{
        //    IDatabase redisDB = _redisConnection.GetDatabase();

        //    int fromId = senderId > receiverId ? senderId : receiverId;
        //    int toId = senderId < receiverId ? senderId : receiverId;

        //    await redisDB.KeyExpireAsync($"messages:{fromId}:{toId}:chat", new TimeSpan(48, 0, 0));
        //}

        //public async Task<int> GetTimeToLiveForStream(int senderId, int receiverId)
        //{
        //    IDatabase redisDB = _redisConnection.GetDatabase();

        //    int biggerId = senderId > receiverId ? senderId : receiverId;
        //    int smallerId = senderId < receiverId ? senderId : receiverId;
        //    TimeSpan? timeToLive = await redisDB.KeyTimeToLiveAsync($"messages:{biggerId}:{smallerId}:chat");
        //    return (int)timeToLive.Value.TotalSeconds;
        //}

        public async Task<IEnumerable<Rentee>> GetRentererInChatWith(int rentererId)
        {
            List<Rentee> rentees = new List<Rentee>();
            string setKey = $"renterer:{rentererId}:chats";
            IDatabase redisDB = _redisConnection.GetDatabase();
            var renteeSetEntries = await redisDB.SortedSetRangeByRankAsync(setKey, 0, -1, Order.Descending);
            foreach (var entry in renteeSetEntries)
            {
                Rentee rentee = JsonSerializer.Deserialize<Rentee>(entry);
                rentees.Add(rentee);
            }
            return rentees;
        }

        public async Task<IEnumerable<Renterer>> GetRenteeInChatWith(int renteeId)
        {
            List<Renterer> renterers = new List<Renterer>();
            string setKey = $"rentee:{renteeId}:chats";
            IDatabase redisDB = _redisConnection.GetDatabase();
            var rentererSetEntries = await redisDB.SortedSetRangeByRankAsync(setKey, 0, -1, Order.Descending);
            foreach (var entry in rentererSetEntries)
            {
                Renterer renterer = JsonSerializer.Deserialize<Renterer>(entry);
                renterers.Add(renterer);
            }
            return renterers;
        }

        public async Task<IEnumerable<int>> GetIdsRentererInChatWith(int rentererId)
        {
            List<int> renteeIds = new List<int>();
            string setKey = $"renterer:{rentererId}:chats";
            IDatabase redisDB = _redisConnection.GetDatabase();
            var renteeSetEntries = await redisDB.SortedSetRangeByRankAsync(setKey, 0, -1, Order.Descending);
            foreach (var entry in renteeSetEntries)
            {
                Rentee rentee = JsonSerializer.Deserialize<Rentee>(entry);
                renteeIds.Add(rentee.Id);
            }
            return renteeIds;
        }

        public async Task<IEnumerable<int>> GetIdsRenteeInChatWith(int renteeId)
        {
            List<int> renteeIds = new List<int>();
            string setKey = $"rentee:{renteeId}:chats";
            IDatabase redisDB = _redisConnection.GetDatabase();
            var renteeSetEntries = await redisDB.SortedSetRangeByRankAsync(setKey, 0, -1, Order.Descending);
            foreach (var entry in renteeSetEntries)
            {
                Rentee rentee = JsonSerializer.Deserialize<Rentee>(entry);
                renteeIds.Add(rentee.Id);
            }
            return renteeIds;
        }

        public async Task DeleteConversation(int fromId, int toId)
        {
            IDatabase redisDB = _redisConnection.GetDatabase();
            string key = "messages:" + fromId + ":" + toId + ":chat";
            await redisDB.KeyDeleteAsync(key);
        }
    }
}
