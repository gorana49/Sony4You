using back.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.IService
{
    public interface IReservationService
    {
        public Task AddNewReservationRequest(ReservationPreviewDTO reservation);

        public Task CancelReservationRequest(ReservationPreviewDTO reservation);

        public Task ApproveReservationRequest(ReservationPreviewDTO reservation);

        public Task<IEnumerable<ReservationPreviewDTO>> GetAllReservationRequestForSony(string serialNumber);
    }
}
