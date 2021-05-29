using back.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface IRedisRepository
    {
        public Task AddNewLoggedUser(LoggedUserDTO user);
        public Task RemoveLoggedUser(LoggedUserDTO user);
        public Task<LoggedUserDTO> LogInUser(LoggedUserDTO user);
        public Task LogOutUser(LoggedUserDTO user);
        public Task<bool> CheckIfUserIsLoggedIn(LoggedUserDTO user);
        public Task PushNotification(NotificationDTO notification);
        //Task<string> GetCacheValueAsync(string key);
        //Task SetCacheValueAsync(string key, string value);
    }
}
