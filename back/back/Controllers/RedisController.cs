using back.DtoModels;
using back.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;
        private readonly IFriendRequestRepository _friendRepository;

        public RedisController(IRedisService redisService, IFriendRequestRepository frendRepo)
        {
            _redisService = redisService;
            _friendRepository = frendRepo;
        }

        [HttpPost]
        public async Task AddNewLoggedUser([FromBody] LoggedUserDTO user)
        {
            await _redisService.AddNewLoggedUser(user);
        }

        [HttpDelete]
        public async Task RemoveLoggedUser([FromBody] LoggedUserDTO user)
        {
            await _redisService.RemoveLoggedUser(user);
        }
        [HttpGet]
        public async Task<List<LoggedUserDTO>> GetAllLoggedUsers()
        {

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
        [HttpPost]
        public async Task SendFriendRequest([FromBody] FriendRequest request)
        {
            await _friendRepository.SendFriendRequest(request.senderId, request.receiverId, request.sender);
        }
        [HttpGet]
        public async Task<IEnumerable<Request>> GetFriendRequests([FromQuery] string receiverId)
        {
            return await _friendRepository.GetFriendRequests(int.Parse(receiverId));
        }
    }
}
