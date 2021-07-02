using back.DtoModels;
using System.Collections.Generic;
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
        public Task<List<LoggedUserDTO>> GetAllLoggedUsers();
        public Task PushNotification(NotificationDTO notification);
        public Task UpdatePassword(LoggedUserDTO user);
    }
}
