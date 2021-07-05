using back.DtoModels;
using back.IRepository;
using back.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace back.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;
        public MessageRepository(IRedisConnectionBuilder builder)
        {
            _redisConnection = builder.Connection;
        }
        public async Task SendMessage(MessageDTO message)
        {
            var values = new NameValueEntry[]
            {
                new NameValueEntry("clientuniqueid", message.clientuniqueid),
                new NameValueEntry("sender_username", message.senderUsername),
                new NameValueEntry("receiver_id", message.receiverId),
                new NameValueEntry("receiver_username", message.receiverUsername),
                new NameValueEntry("type", message.type),
                new NameValueEntry("message", message.message),
                new NameValueEntry("date", message.date.ToString()),
            };
            IDatabase redisDB = _redisConnection.GetDatabase();
            string fromUsername;
            string toUsername;
            int proba = string.Compare(message.senderUsername, message.receiverUsername);
            if (proba > 0)
            {
                fromUsername = message.receiverUsername;
                toUsername = message.senderUsername;
            }
            else
            {
                fromUsername = message.senderUsername;
                toUsername = message.receiverUsername;
            }
            await redisDB.StreamAddAsync($"messages:{fromUsername}:{toUsername}:chat", values);
        }
        public async Task SendNotification(NotificationDTO notiffication)
        {
            var values = new NameValueEntry[]
            {
                new NameValueEntry("sender_username", notiffication.SenderUsername),
                new NameValueEntry("receiver_username", notiffication.ReceiverUsername),
                new NameValueEntry("message", notiffication.Message),
            };
            IDatabase redisDB = _redisConnection.GetDatabase();
            await redisDB.StreamAddAsync($"notification:{notiffication.SenderUsername}:{notiffication.ReceiverUsername}:reservation", values);
        }
        public async Task<IEnumerable<NotificationDTO>> ReceiveNotiffication(string senderUsername, string receiverUsername, string from, int count)
        {
            List<NotificationDTO> retMessages = new List<NotificationDTO>();

            string channelName = $"notification:{senderUsername}:{receiverUsername}:reservation";

            IDatabase redisDb = _redisConnection.GetDatabase();
            var mess1 = redisDb.StreamRead(channelName, "0-0");
            from = Uri.UnescapeDataString(from);
            var messages = redisDb.StreamRead(channelName, "0-0", count: count);
            foreach (var message in messages)
            {
                NotificationDTO mess = new NotificationDTO
                {
                    SenderUsername = message.Values.FirstOrDefault(value => value.Name == "sender_username").Value,
                    ReceiverUsername = message.Values.FirstOrDefault(value => value.Name == "receiver_username").Value,
                    Message = message.Values.FirstOrDefault(value => value.Name == "message").Value
                };
                retMessages.Add(mess);
            }
            return retMessages;
        }

        public async Task<IEnumerable<MessageDTO>> ReceiveMessage(string senderUsername, string receiverUsername, string from, int count)
        {
            List<MessageDTO> retMessages = new List<MessageDTO>();
            string fromUsername;
            string toUsername;
            int proba = string.Compare(senderUsername, receiverUsername);
            if (proba > 0)
            {
                fromUsername = receiverUsername;
                toUsername = senderUsername;
            }
            else
            {
                fromUsername = senderUsername;
                toUsername = receiverUsername;
            }
            string channelName = $"messages:{fromUsername}:{toUsername}:chat";

            IDatabase redisDb = _redisConnection.GetDatabase();
            var mess1 = redisDb.StreamRead(channelName, "0-0");
            from = Uri.UnescapeDataString(from);
            var messages = redisDb.StreamRead(channelName, "0-0", count: count);
            foreach (var message in messages)
            {
                MessageDTO mess = new MessageDTO
                {
                    clientuniqueid = message.Values.FirstOrDefault(value => value.Name == "clientuniqueid").Value,
                    senderUsername = message.Values.FirstOrDefault(value => value.Name == "sender_username").Value,
                    receiverId = message.Values.FirstOrDefault(value => value.Name == "receiver_id").Value,
                    receiverUsername = message.Values.FirstOrDefault(value => value.Name == "receiver_username").Value,
                    type = message.Values.FirstOrDefault(value => value.Name == "type").Value,
                    message = message.Values.FirstOrDefault(value => value.Name == "message").Value,
                    date = DateTime.Parse(message.Values.FirstOrDefault(value => value.Name == "date").Value.ToString())
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
