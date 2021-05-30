using back.DtoModels;
using back.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back
{
    public interface IRedisService
    {
        public Task AddNewLoggedUser(LoggedUserDTO user);
        public Task RemoveLoggedUser(LoggedUserDTO user);
        public Task<LoggedUserDTO> LogInUser(LoggedUserDTO user); 
        public Task LogOutUser(LoggedUserDTO user);
        public Task<LoggedUserDTO> CheckIfUserIsValid(LoggedUserDTO user);
        public Task<LoggedUserDTO> CheckIfUserIsLoggedIn(LoggedUserDTO user);
        public Task PushNotification(NotificationDTO notification);
        public Task UpdatePassword(LoggedUserDTO user);
    }
}
