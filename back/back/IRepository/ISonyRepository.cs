using back.DtoModels;
using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface ISonyRepository
    {
        public Task AddSony(string Username, Sony sony);
        public Task DeleteSony(string SerialNumber);
        public Task UpdateSony(string Price, string SerialNumber);
        public Task<List<Sony>> GetMySonys(string Username);
        public Task<Sony> GetSonyBySerialNumber(string SerialNumber);
        public Task<Sony> ReservedMySony(string SerialNumber, string UsernameRentee, RenterListDTO renterList);
        public Task<List<ReservationPreviewDTO>> GetReservedSonys(string UsernameRenterer);
        public Task<List<AvailableSonyDTO>> GetAvailableSonys();
    }
}
