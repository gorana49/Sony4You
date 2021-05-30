using back.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface IReservationRepository
    {
        public Task AddNewReservationRequest(ReservationPreviewDTO reservation); 
        public Task ApproveReservationRequest(ReservationPreviewDTO reservation);
        public Task<IEnumerable<ReservationPreviewDTO>> GetAllReservationRequestForSony(string serialNumber);
        public Task CancelReservationRequest(ReservationPreviewDTO reservation);
    }
}
