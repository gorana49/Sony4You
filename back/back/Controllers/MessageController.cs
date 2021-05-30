using back.DtoModels;
using back.IRepository;
using back.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _repository;
        public MessageController(IMessageRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] Message message)
        {
            await _repository.SendMessage(message);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ReceiveMessage([FromQuery] int senderId, [FromQuery] int receiverId, [FromQuery] string from, [FromQuery] int count)
        {
            var messages = await _repository.ReceiveMessage(senderId, receiverId, from, count);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult> StartConversation([FromBody] ConversationDTO participants)
        {
            try
            {
                await _repository.StartConversation(participants);
                var message = new Message
                {
                    SenderUsername = $"{participants.Sender.Username}",
                    SenderId = participants.Sender.Id,
                    ReceiverUsername = $"{participants.Receiver.Username}",
                    ReceiverId = participants.Receiver.Id,
                    Text = participants.Text
                };

                await _repository.SendMessage(message);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetRentererInChatWith(int rentererId)
        {
            var rentees = await _repository.GetRentererInChatWith(rentererId);
            return Ok(rentees);
        }

        [HttpGet]
        public async Task<ActionResult> GetIdsRentererInChatWith(int rentererId)
        {
            var ids = await _repository.GetIdsRentererInChatWith(rentererId);
            return Ok(ids);
        }

        [HttpGet]
        public async Task<ActionResult> GetRenteesInChatWith(int renteeId)
        {
            var rentees = await _repository.GetRentererInChatWith(renteeId);
            return Ok(rentees);
        }

        [HttpGet]
        public async Task<ActionResult> GetIdsRenteesInChatWith(int renteeId)
        {
            var ids = await _repository.GetIdsRentererInChatWith(renteeId);
            return Ok(ids);
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteConversation(int fromId, int toId)
        {
            await _repository.DeleteConversation(fromId, toId);
            return Ok();
        }
    }
}
