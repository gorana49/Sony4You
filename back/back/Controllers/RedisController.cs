using back.DtoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public RedisController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpPost]
        public async Task AddNewLoggedUser([FromBody] LoggedUserDTO user)
        {
            await _redisService.AddNewLoggedUser(user);
        }

        [HttpPost]
        public async Task RemoveLoggedUser([FromBody] LoggedUserDTO user)
        {
            await _redisService.RemoveLoggedUser(user);
        }

        [HttpPost]
        public async Task<IActionResult> LogInUser([FromBody] LoggedUserDTO user)
        {
            LoggedUserDTO value = await _redisService.LogInUser(user);
            return value == null ? (IActionResult)NotFound() : Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> LogOutUser([FromBody] LoggedUserDTO user)
        {
            await _redisService.LogOutUser(user);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CheckIfUserIsValid([FromBody] LoggedUserDTO user)
        {
            var value = await _redisService.CheckIfUserIsValid(user);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CheckIfUserIsLoggedIn([FromBody] LoggedUserDTO user)
        {
            var value = await _redisService.CheckIfUserIsLoggedIn(user);
            return Ok(value);
        }

        [HttpPost]
        public async Task PushNotification([FromBody] NotificationDTO notification)
        {
            await _redisService.PushNotification(notification);
        }

        [HttpPost]
        public async Task UpdatePassword([FromBody] LoggedUserDTO user)
        {
            await _redisService.UpdatePassword(user);
        }
    }
}
