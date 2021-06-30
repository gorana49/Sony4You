using back.DtoModels;
using back.IRepository;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace back.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ISonyRepository _sonyRepository;

        public ReservationRepository(IRedisConnectionBuilder connectionBuilder, ISonyRepository sonyRepo)
        {
            _connectionMultiplexer = connectionBuilder.Connection;
            _sonyRepository = sonyRepo;
        }

        public async Task AddNewReservationRequest(ReservationPreviewDTO reservation)
        {
            var db = _connectionMultiplexer.GetDatabase();
            db.StreamAdd($"reservation:{reservation.SerialNumber}:request", reservation.UsernameRentee, JsonSerializer.Serialize(reservation));
        }

        public async Task CancelReservationRequest(ReservationPreviewDTO reservation)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var reservations = db.StreamRead($"reservation:{ reservation.SerialNumber}:request", "0-0");
            RedisValue idEntryUser = new RedisValue();
            bool exists = false;
            foreach (StreamEntry entry in reservations)
            {
                if (entry.Values[0].Name == reservation.UsernameRentee)
                {
                    idEntryUser = entry.Id;
                    var newEntry = JsonSerializer.Deserialize<ReservationPreviewDTO>(entry.Values[0].Value);
                    if (newEntry.RenterList.StartTime == reservation.RenterList.StartTime &&
                        newEntry.RenterList.Period == reservation.RenterList.Period)
                    {
                        exists = true;
                    }
                    break;
                }
            }
            if (exists)
            {
                db.StreamDelete($"reservation:{reservation.SerialNumber}:request", new[] { idEntryUser });
            }
        }

        public async Task ApproveReservationRequest(ReservationPreviewDTO reservation)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var reservations = db.StreamRead($"reservation:{ reservation.SerialNumber}:request", "0-0");
            RedisValue idEntryUser = new RedisValue();
            bool exists = false;
            foreach (StreamEntry entry in reservations)
            {
                if (entry.Values[0].Name == reservation.UsernameRentee)
                {
                    idEntryUser = entry.Id;
                    var newEntry = JsonSerializer.Deserialize<ReservationPreviewDTO>(entry.Values[0].Value);
                    if(newEntry.RenterList.StartTime == reservation.RenterList.StartTime &&
                        newEntry.RenterList.Period == reservation.RenterList.Period)
                    {
                        exists = true;
                    }
                    break;
                }
            }
            if (exists)
            {
                db.StreamDelete($"reservation:{reservation.SerialNumber}:request", new[] { idEntryUser });
                await _sonyRepository.ReservedMySony(reservation.SerialNumber, reservation.UsernameRentee, reservation.RenterList);
            }
        }

        public async Task<IEnumerable<ReservationPreviewDTO>> GetAllReservationRequestForSony(string serialNumber)
        {
            List<ReservationPreviewDTO> requestsResponse = new List<ReservationPreviewDTO>();
            string setKey = $"reservation:{serialNumber}:request";
            IDatabase redisDB = _connectionMultiplexer.GetDatabase();
            var requests = redisDB.StreamRead(setKey, "0-0");
            foreach (var entry in requests)
            {
                ReservationPreviewDTO req = JsonSerializer.Deserialize<ReservationPreviewDTO>(entry.Values[0].Value);
                requestsResponse.Add(req);
            }
            return requestsResponse;
        }

        
    }
}
