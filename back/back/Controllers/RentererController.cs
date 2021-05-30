using back.DtoModels;
using back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RentererController : ControllerBase
    {
        private readonly IRentererService _rentererService;
        public RentererController(IRentererService rentererService)
        {
            _rentererService = rentererService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRenterer([FromBody] Renterer renterer)
        {
            await _rentererService.AddRenterer(renterer);
            return StatusCode(201, "Node has succesifully added to db");
        }
        [HttpGet]
        public async Task<List<Renterer>> GetAllRenterers()
        {
            return await _rentererService.GetAllRenterers();
        }

        [HttpGet]
        public async Task<Renterer> GetRenterer([FromQuery] string CompanyName)
        {
            return await _rentererService.GetRenterer(CompanyName);
        }
        [HttpDelete]
        public Task DeleteRenterer([FromQuery] string Name)
        {
            return _rentererService.DeleteRenterer(Name);
        }
        [HttpPut]
        public Task UpdateRenterer([FromBody] UpdateRentererDTO renterer)
        {
            return _rentererService.UpdateRenterer(renterer);
        }
        [HttpPost]
        public Task AddSony([FromBody] AddSonyDTO Addsony)
        {
            Sony sony = new Sony(Addsony.SerialNumber, Addsony.Type, Addsony.Price, Addsony.Notes);
            return _rentererService.AddSony(Addsony.RentererUsername, sony);
        }
        [HttpDelete]
        public Task DeleteSony([FromQuery] string SerialNumber)
        {
            return _rentererService.DeleteSony(SerialNumber);
        }
        [HttpPut]
        public Task UpdateSony([FromQuery] string Price, string SerialNumber)
        {
            return _rentererService.UpdateSony(Price, SerialNumber);
        }
        [HttpGet]
        public Task<List<Sony>> GetMySonys([FromQuery] string Username)
        {
            return _rentererService.GetMySonys(Username);
        }
        [HttpGet]
        public Task<Sony> GetSonyBySerialNumber([FromQuery] string SerialNumber)
        {
            return _rentererService.GetSonyBySerialNumber(SerialNumber);
        }
        //[HttpPost]
        //public Task AddContest(ContestRenterer conret)
        //{

        //}

        [HttpPut]
        public Task UpdateContest([FromBody] RentererContestDTO rcon)
        {
            Contest cont = new Contest(rcon.Name, rcon.Date, rcon.NumOfPlayers, rcon.Price, rcon.Award);
            return _rentererService.UpdateContest(rcon.UsernameRenterer, cont);
        }

        [HttpDelete]
        public Task DeleteContest([FromQuery] string Name)
        {
            return _rentererService.DeleteContest(Name);
        }
        [HttpGet]
        public Task<List<Contest>> GetAllContest()
        {
            return _rentererService.GetAllContest();
        }
        [HttpGet]
        public Task<Contest> GetContest([FromQuery] string Name)
        {
            return _rentererService.GetContest(Name);
        }
        //[HttpPost]
        //public Task AddGame(string SerialNumber, Game game)
        //{


        //}
        //[HttpPut]
        //public Task UpdateGame(string Players, Game gam)
        //{

        //}
        [HttpDelete]
        public Task DeleteGame([FromQuery] string Name)
        {
            return _rentererService.DeleteGame(Name);
        }
        [HttpGet]
        public Task<List<Game>> GetGamesOnSony([FromQuery] string SerialNumber)
        {
            return _rentererService.GetGamesOnSony(SerialNumber);
        }
        [HttpPost]
        public Task<Sony> ReservedMySony(ReservationPreviewDTO reservationPreviewDTO)
        {
            return _rentererService.ReservedMySony(reservationPreviewDTO.SerialNumber, reservationPreviewDTO.UsernameRentee, reservationPreviewDTO.RenterList);
        }
        [HttpGet]
        public Task<List<ReservationPreviewDTO>> GetReservedSonys(string UsernameRenterer)
        {
            return _rentererService.GetReservedSonys(UsernameRenterer);
        }
        [HttpPost]
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            return _rentererService.AddRequest(SenderRequestUsername, ReceiverRequestUsername);
        }
        [HttpDelete]
        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            return _rentererService.DeleteRequest(SenderRequestUsername, ReceiverRequestUsername);
        }
        [HttpPost]
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername)
        {
            return _rentererService.MakeUsFriends(SenderUsername, ReceiverUsername);
        }
        [HttpDelete]
        public Task CancelReservation([FromBody] ReservationPreviewDTO preview)
        {
            return _rentererService.CancelReservation(preview);
        }
    }
}
