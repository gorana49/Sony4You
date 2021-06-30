using back.DtoModels;
using back.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task AddNewReservationRequest([FromBody] ReservationPreviewDTO reservation)
        {
            await _reservationService.AddNewReservationRequest(reservation);
        }
        [HttpPost]
        public async Task CancelReservation([FromBody] ReservationPreviewDTO reservation)
        {
            await _reservationService.CancelReservationRequest(reservation);
        }
        [HttpPost]
        public async Task ApproveReservationRequest([FromBody] ReservationPreviewDTO reservation)
        {
            await _reservationService.ApproveReservationRequest(reservation);
        }

        [HttpGet]
        public async Task<IEnumerable<ReservationPreviewDTO>> GetAllReservationRequestForSony([FromQuery] string serialNumber)
        {
            return await _reservationService.GetAllReservationRequestForSony(serialNumber);
        }
    }
}
