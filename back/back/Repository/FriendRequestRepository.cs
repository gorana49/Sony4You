using back.DtoModels;
using back.IRepository;
using Neo4jClient;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace back.Repository
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly IBoltGraphClient _client;
        private readonly IConnectionMultiplexer _redisConnection;
        public FriendRequestRepository(IBoltGraphClient client, IRedisConnectionBuilder builder)
        {
            _client = client;
            _redisConnection = builder.Connection;
        }
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            var result = _client.Cypher.Match(@"(sender)").Where("(sender.Username = $SenderRequestUsername)")
                .WithParam("SenderRequestUsername", SenderRequestUsername)
                .Match(@"(receiver)")
                .Where("(receiver.Username = $ReceiverRequestUsername)")
                .WithParam("ReceiverRequestUsername", ReceiverRequestUsername)
                .Create("(sender)-[r:FRIEND_REQUEST] -> (receiver)")
                .ExecuteWithoutResultsAsync();
            return result;
        }

        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            var result = _client.Cypher.Match(@"(sender) - [r:FRIEND_REQUEST]-> (receiver)")
                .Where("(sender.Username = $SenderRequestUsername)")
                .WithParam("SenderRequestUsername", SenderRequestUsername)
                .AndWhere("(receiver.Username = $ReceiverRequestUsername)")
                .WithParam("ReceiverRequestUsername", ReceiverRequestUsername)
                .DetachDelete("r")
                .ExecuteWithoutResultsAsync();
            return result;
        }
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername)
        {
            var result = _client.Cypher.Match(@"(sender), (receiver)").Where("(sender.Username = $SenderRequestUsername)")
               .WithParam("SenderRequestUsername", SenderUsername)
               .AndWhere("(receiver.Username = $ReceiverRequestUsername)").WithParam("ReceiverRequestUsername", ReceiverUsername)
                .Create("(sender)-[r:FRIENDS] - (receiver)")
                .ExecuteWithoutResultsAsync();
            return result;
        }

        public Task DeleteMyFriend(string SenderUsername, string ReceiverUsername)
        {
            var result = _client.Cypher.Match(@"(sender), (receiver)").Where("(sender.Username = $SenderRequestUsername)")
              .WithParam("SenderRequestUsername", SenderUsername)
              .AndWhere("(receiver.Username = $ReceiverRequestUsername)").WithParam("ReceiverRequestUsername", ReceiverUsername)
              .Match("(sender)-[r:FRIENDS]-(receiver)")
              .Delete("r")
              .ExecuteWithoutResultsAsync();
            return result;
        }

        public async Task SendFriendRequest(int senderId, int receiverId, Request sender)
        {
            string channelName = $"messages:{receiverId}:friend_request";

            var values = new NameValueEntry[]
            {
                new NameValueEntry("sender_id", senderId),
                new NameValueEntry("sender_first_name", sender.FirstName),
                new NameValueEntry("sender_last_name", sender.LastName),
                new NameValueEntry("sender_username", sender.Username)
            };

            IDatabase redisDB = _redisConnection.GetDatabase();
            var messageId = await redisDB.StreamAddAsync(channelName, values);
            await redisDB.SetAddAsync("friend:" + senderId + ":request", receiverId);
            FriendRequestNotificationDTO message = new FriendRequestNotificationDTO
            {
                ReceiverId = receiverId,
                IdNotification = messageId,
                Request = sender
            };
            var jsonMessage = JsonSerializer.Serialize(message);
            ISubscriber pubNotification = _redisConnection.GetSubscriber();
            await pubNotification.PublishAsync("friendship.requests", jsonMessage);
        }

        public async Task<IEnumerable<Request>> GetFriendRequests(int receiverId)
        {
            string channelName = $"messages:{receiverId}:friend_request";
            IDatabase redisDB = _redisConnection.GetDatabase();

            var requests = await redisDB.StreamReadAsync(channelName, "0-0");

            IList<Request> result = new List<Request>();

            foreach (var request in requests)
            {
                result.Add(
                    new Request
                    {
                        Id = int.Parse(request.Values.FirstOrDefault(value => value.Name == "sender_id").Value),
                        FirstName = request.Values.FirstOrDefault(value => value.Name == "sender_first_name").Value,
                        LastName = request.Values.FirstOrDefault(value => value.Name == "sender_last_name").Value,
                        Username = request.Values.FirstOrDefault(value => value.Name == "sender_username").Value,
                    });
            }
            return result;
        }

    }
}
