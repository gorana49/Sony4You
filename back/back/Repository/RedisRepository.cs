using back.DtoModels;
using back.IRepository;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

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
                if (entry.Values[0].Name == user.Username)
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                user.LoggedIn = false;
                db.StreamAdd("logged_users", user.Username, JsonSerializer.Serialize<LoggedUserDTO>(user));
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
                if (entry.Values[0].Name == user.Username)
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
                if (entry.Values[0].Name == user.Username)
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
                db.StreamAdd("logged_users", entryUser.Username, JsonSerializer.Serialize<LoggedUserDTO>(entryUser));

                //var subscriber = _connectionMultiplexer.GetSubscriber();
                //subscriber.Subscribe($"notifications:{entryUser.Id}").OnMessage(message =>
                //{
                //    NotificationDTO deserializedMessage = JsonSerializer.Deserialize<NotificationDTO>(message.Message);
                //    string groupName = $"notification:{deserializedMessage.ReceiverId}";
                //    //_ = _hub.Clients.Group(groupName).SendAsync("ReceiveFriendRequests", deserializedMessage);

                //    //string groupName = $"requests:{deserializedMessage.ReceiverId}:friendship";

                //});
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
                if (entry.Values[0].Name == user.Username)
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
                db.StreamAdd("logged_users", entryUser.Username, JsonSerializer.Serialize<LoggedUserDTO>(entryUser));

            }

        }

        public async Task<LoggedUserDTO> CheckIfUserIsValid(LoggedUserDTO user)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var users = db.StreamRead("logged_users", "0-0");
            foreach (StreamEntry entry in users)
            {
                if (entry.Values[0].Name == user.Username)
                {
                    LoggedUserDTO entryUser = JsonSerializer.Deserialize<LoggedUserDTO>(entry.Values[0].Value.ToString());
                    if (entryUser.Password == user.Password)
                        return entryUser;
                }
            }
            return null;
        }

        public async Task<LoggedUserDTO> CheckIfUserIsLoggedIn(LoggedUserDTO user)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var users = db.StreamRead("logged_users", "0-0");
            foreach (StreamEntry entry in users)
            {
                if (entry.Values[0].Name == user.Username)
                {
                    LoggedUserDTO entryUser = JsonSerializer.Deserialize<LoggedUserDTO>(entry.Values[0].Value.ToString());
                    if (entryUser.Password == user.Password && entryUser.LoggedIn)
                        return entryUser;
                }
            }
            return null;
        }

        public async Task PushNotification(NotificationDTO notification)
        {
            var publisher = _connectionMultiplexer.GetSubscriber();
            publisher.Publish($"notification:{notification.ReceiverId}", JsonSerializer.Serialize<NotificationDTO>(notification));
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
