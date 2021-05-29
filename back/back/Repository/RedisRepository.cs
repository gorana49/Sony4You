using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.DtoModels;
using back.IRepository;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace back.Repository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisRepository(IRedisConnectionBuilder connectionBuilder)
        {
            _connectionMultiplexer = connectionBuilder.Connection;
        }

        public async Task AddNewLoggedUser(LoggedUserDTO user)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var users = db.StreamRead("logged_users", "0-0");
            bool exists = false;
            foreach (StreamEntry entry in users)
            {
                if (entry.Values[0].Name == user.UserId)
                {
                    exists = true;
                }
            }
            if(!exists)
            {
                user.LoggedIn = false;
                db.StreamAdd("logged_users", user.UserId, JsonSerializer.Serialize<LoggedUserDTO>(user));
            }
        }

        public async Task RemoveLoggedUser(LoggedUserDTO user)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var users = db.StreamRead("logged_users", "0-0");
            bool exists = false;
            RedisValue val = new RedisValue();
            foreach (StreamEntry entry in users)
            {
                if (entry.Values[0].Name == user.UserId)
                {
                    exists = true;
                    val = entry.Id;
                    break;
                }
            }
            if (exists)
            {
                db.StreamDelete("logged_users", new[] { val });
            }
        }

        public async Task<LoggedUserDTO> LogInUser(LoggedUserDTO user)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var users = db.StreamRead("logged_users", "0-0");
            LoggedUserDTO entryUser = new LoggedUserDTO();
            RedisValue idEntryUser = new RedisValue();
            bool exists = false;
            foreach (StreamEntry entry in users)
            {
                if (entry.Values[0].Name == user.UserId)
                {
                    exists = true;
                    idEntryUser = entry.Id;
                    entryUser = JsonSerializer.Deserialize<LoggedUserDTO>(entry.Values[0].Value.ToString());
                    break;
                }
            }
            if (!exists)
            {
                return null;
            }
            else
            {
                db.StreamDelete("logged_users", new[] { idEntryUser });
                entryUser.LoggedIn = true;
                db.StreamAdd("logged_users", entryUser.UserId, JsonSerializer.Serialize<LoggedUserDTO>(entryUser));

                var subscriber = _connectionMultiplexer.GetSubscriber();
                subscriber.Subscribe($"notifications:{entryUser.UserId}").OnMessage(message =>
                {
                    NotificationDTO deserializedMessage = JsonSerializer.Deserialize<NotificationDTO>(message.Message);
                    string groupName = $"notification:{deserializedMessage.ReceiverId}";
                    //_ = _hub.Clients.Group(groupName).SendAsync("ReceiveFriendRequests", deserializedMessage);

                    //string groupName = $"requests:{deserializedMessage.ReceiverId}:friendship";

                });
                return entryUser;
            }

        }

        public async Task LogOutUser(LoggedUserDTO user)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var users = db.StreamRead("logged_users", "0-0");
            LoggedUserDTO entryUser = new LoggedUserDTO();
            RedisValue idEntryUser = new RedisValue();
            bool exists = false;
            foreach (StreamEntry entry in users)
            {
                if (entry.Values[0].Name == user.UserId)
                {
                    exists = true;
                    idEntryUser = entry.Id;
                    entryUser = JsonSerializer.Deserialize<LoggedUserDTO>(entry.Values[0].Value.ToString());
                    break;
                }
            }
            if (exists)
            {
                db.StreamDelete("logged_users", new[] { idEntryUser });
                entryUser.LoggedIn = false;
                db.StreamAdd("logged_users", entryUser.UserId, JsonSerializer.Serialize<LoggedUserDTO>(entryUser));

            }
            
        }

        public async Task<bool> CheckIfUserIsLoggedIn(LoggedUserDTO user)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var users = db.StreamRead("logged_users", "0-0");
            foreach (StreamEntry entry in users)
            {
                if (entry.Values[0].Name == user.UserId)
                {
                    LoggedUserDTO entryUser = JsonSerializer.Deserialize<LoggedUserDTO>(entry.Values[0].Value.ToString());
                    return entryUser.LoggedIn;
                }
            }
            return false;
        }

        public async Task PushNotification(NotificationDTO notification)
        {
            var publisher = _connectionMultiplexer.GetSubscriber();
            publisher.Publish($"notification:{notification.ReceiverId}",JsonSerializer.Serialize<NotificationDTO>(notification));
        }

        //public async Task<string> GetCacheValueAsync(string key)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    return await db.StringGetAsync(key);
        //}

        //public async Task SetCacheValueAsync(string key, string value)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    await db.StringSetAsync(key, value);
        //}
    }
}
