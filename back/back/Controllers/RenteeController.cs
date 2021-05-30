using back.DtoModels;
using back.IService;
using back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RenteeController : ControllerBase
    {
        private readonly IRenteeService _renteeService;
        public RenteeController(IRenteeService renteeService)
        {
            _renteeService = renteeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRentee([FromBody] Rentee rentee)
        {
            await _renteeService.AddRentee(rentee);
            return StatusCode(201, "Node has succesifully added to db");
        }
        [HttpGet]
        public async Task<List<Rentee>> GetAllRentees()
        {
            return await _renteeService.GetAllRentees();
        }

        [HttpGet]
        public async Task<Rentee> GetRentee([FromQuery] string Username)
        {
            return await _renteeService.GetRentee(Username);
        }
        [HttpDelete]
        public Task DeleteRentee([FromQuery] string Username)
        {
            return _renteeService.DeleteRentee(Username);
        }
        [HttpPut]
        public Task UpdateRenterer([FromBody] UpdateRenteeDTO rentee)
        {
            return _renteeService.UpdateRentee(rentee);
        }

        [HttpPost]
        public Task AddCommentToRenterer([FromBody] AddCommentDTO addCommentDto)
        {
            return _renteeService.AddCommentToRenterer(addCommentDto.Comment, addCommentDto.Username, addCommentDto.UsernameRentee);
        }
        [HttpDelete]
        public Task DeleteComment([FromQuery] string title)
        {
            return _renteeService.DeleteComment(title);
        }
        [HttpGet]
        public Task<List<Comment>> GetCommentRentee([FromQuery] string UsernameRentee, string UsernameRenterer)
        {
            return _renteeService.GetCommentRentee(UsernameRentee, UsernameRenterer);
        }
        [HttpPut]
        public Task UpdateComment(string Text, string Title)
        {
            return this._renteeService.UpdateComment(Text, Title);
        }
        [HttpGet]
        public Task<List<AvailableSonyDTO>> GetAvailableSonys()
        {
            return this._renteeService.GetAvailableSonys();
        }
        [HttpPost]
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            return _renteeService.AddRequest(SenderRequestUsername, ReceiverRequestUsername);
        }
        [HttpDelete]
        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            return _renteeService.DeleteRequest(SenderRequestUsername, ReceiverRequestUsername);
        }
        [HttpPost]
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername)
        {
            return _renteeService.MakeUsFriends(SenderUsername, ReceiverUsername);
        }
    }
}
