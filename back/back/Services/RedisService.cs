using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.DtoModels;
using back.IRepository;
using StackExchange.Redis;

namespace back
{
    public class RedisService : IRedisService
    {
        private readonly IRedisRepository _redisRepository;

        public RedisService(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public async Task AddNewLoggedUser(LoggedUserDTO user)
        {
            await _redisRepository.AddNewLoggedUser(user);
        }

        public async Task RemoveLoggedUser(LoggedUserDTO user)
        {
            await _redisRepository.RemoveLoggedUser(user);
        }

        public async Task<LoggedUserDTO> LogInUser(LoggedUserDTO user)
        {
            return await this._redisRepository.LogInUser(user);
        }

        public async Task LogOutUser(LoggedUserDTO user)
        {
            await this._redisRepository.LogOutUser(user);
        }

        public async Task<LoggedUserDTO> CheckIfUserIsValid(LoggedUserDTO user)
        {
            return await this._redisRepository.CheckIfUserIsValid(user);
        }

        public async Task<LoggedUserDTO> CheckIfUserIsLoggedIn(LoggedUserDTO user)
        {
            return await this._redisRepository.CheckIfUserIsLoggedIn(user);
        }


        public async Task PushNotification(NotificationDTO notification)
        {
            await _redisRepository.PushNotification(notification);
        }

        public async Task UpdatePassword(LoggedUserDTO user)
        {
            await _redisRepository.UpdatePassword(user);
        }
    }
}
