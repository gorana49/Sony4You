using back.IRepository;
using back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ForumController : ControllerBase
    {
        private readonly IForumRepository _forumRepository;
        public ForumController(IForumRepository repository)
        {
            _forumRepository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] Message message)
        {
            await _forumRepository.SendMessage(message);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ReceiveMessage([FromQuery] int senderId, [FromQuery] int receiverId, [FromQuery] string from, [FromQuery] int count)
        {
            var messages = await _forumRepository.ReceiveMessage(senderId, receiverId, from, count);
            return Ok(messages);
        }
    }
}
