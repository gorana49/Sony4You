using back.DtoModels;
using back.IRepository;
using back.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task AddNewReservationRequest(ReservationPreviewDTO reservation)
        {
            await _reservationRepository.AddNewReservationRequest(reservation);
        }

        public async Task CancelReservationRequest(ReservationPreviewDTO reservation)
        {
            await _reservationRepository.CancelReservationRequest(reservation);
        }

        public async Task ApproveReservationRequest(ReservationPreviewDTO reservation)
        {
            await _reservationRepository.ApproveReservationRequest(reservation);
        }

        public async Task<IEnumerable<ReservationPreviewDTO>> GetAllReservationRequestForSony(string serialNumber)
        {
            return await _reservationRepository.GetAllReservationRequestForSony(serialNumber);
        }
    }
}
