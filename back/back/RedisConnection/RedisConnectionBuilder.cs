using back.HubConfiguration;
using back.Models;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Text.Json;

namespace back
{
    public class RedisConnectionBuilder : IRedisConnectionBuilder
    {
        private static IConnectionMultiplexer _connection = null;
        private static object _objectLock = new object();
        private static readonly string ConnectionString = "localhost:6379";
        private readonly IHubContext<MessageHub> _hub;

        public RedisConnectionBuilder(IHubContext<MessageHub> hub)
        {
            _hub = hub;
        }

        public IConnectionMultiplexer Connection
        {
            get
            {
                if (_connection == null)
                {
                    lock (_objectLock)
                    {
                        if (_connection == null)
                        {
                            _connection = ConnectionMultiplexer.Connect(ConnectionString);
                            var redisPubSub = _connection.GetSubscriber();

                            redisPubSub.Subscribe("chat.messages").OnMessage(message =>
                            {
                                Message deserializedMessage = JsonSerializer.Deserialize<Message>(message.Message);
                                string groupName = $"channel:{deserializedMessage.ReceiverId}";
                                _ = _hub.Clients.Group(groupName).SendAsync("ReceiveMessage", deserializedMessage);
                            });

                            //redisPubSub.Subscribe("friendship.requests").OnMessage(message =>
                            //{

                            //    NotificationDTO deserializedMessage = JsonSerializer.Deserialize<NotificationDTO>(message.Message);
                            //    string groupName = $"notification:{deserializedMessage.ReceiverId}";

                            //    _ = _hub.Clients.Group(groupName).SendAsync("ReceiveFriendRequests", deserializedMessage);

                            //    string groupName = $"requests:{deserializedMessage.ReceiverId}:friendship";

                            //});
                        }
                    }
                }
                return _connection;
            }
        }
    }
}
