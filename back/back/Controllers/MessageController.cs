using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.IRepository;
using Microsoft.AspNetCore.SignalR;
using back.Models;
using back.DtoModels;

namespace back.Controllers
{
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _repository;
        private readonly IHubContext<MessageHub> _hub;
        public MessageController(IMessageRepository repository, IHubContext<MessageHub> hub)
        {
            _repository = repository;
            _hub = hub;
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> SendMessage([FromBody] Message message)
        {
            await _repository.SendMessage(message);
            return Ok();
        }

        [HttpGet]
        [Route("receive")]
        public async Task<ActionResult> ReceiveMessage([FromQuery] int senderId, [FromQuery] int receiverId, [FromQuery] string from, [FromQuery] int count)
        {
            var messages = await _repository.ReceiveMessage(senderId, receiverId, from, count);
            return Ok(messages);
        }

        [HttpPost]
        [Route("add-conversation/temp")]
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
                //await _repository.SetTimeToLiveForStream(participants.Sender.Id, participants.Receiver.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("time-left/sender/{senderId}/receiver/{receiverId}")]
        //public async Task<ActionResult> GetConversationTimeLeft(int senderId, int receiverId)
        //{
        //    var timeLeft = await _repository.GetTimeToLiveForStream(senderId, receiverId);
        //    return Ok(timeLeft);
        //}

        [HttpGet]
        [Route("chats/student/{studentId}")]
        public async Task<ActionResult> GetStudentsInChatWith(int studentId)
        {
            var students = await _repository.GetRentererInChatWith(studentId);
            return Ok(students);
        }

        [HttpGet]
        [Route("chat-ids/student/{studentId}")]
        public async Task<ActionResult> GetIdsStudentsInChatWith(int studentId)
        {
            var ids = await _repository.GetIdsRentererInChatWith(studentId);
            return Ok(ids);
        }

        [HttpDelete]
        [Route("deleteConversation/user/{biggerId}/{smallerId}")]

        public async Task<IActionResult> DeleteConversation(int biggerId, int smallerId)
        {
            await _repository.DeleteConversation(biggerId, smallerId);
            return Ok();
        }
    }
}
