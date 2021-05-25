using back.DtoModels;
using back.IRepository;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.Repository
{
    public class SonyRepository : ISonyRepository
    {
        private readonly IBoltGraphClient _client;
        public SonyRepository(IBoltGraphClient client)
        {
            _client = client;
        }
        public Task AddSony(string Username, Sony sony)
        {
            var result = _client.Cypher
                .Match(@"(renterer:Renterer)").Where((Renterer renterer) => renterer.Username == Username)
                .Create("(sony:Sony {sony})").WithParams(new { sony })
                .Create("(renterer)-[r:OWN] -> (sony)")
                .ExecuteWithoutResultsAsync();
            return result;
        }
        public Task DeleteSony(string SerialNumber)
        {
            var result = _client.Cypher.Match("(sony:Sony)")
               .Where((Sony sony) => sony.SerialNumber == SerialNumber)
               .DetachDelete("sony")
               .ExecuteWithoutResultsAsync();
            return result;
        }
        public Task UpdateSony(string Price, string SerialNumber)
        {
            var result = _client.Cypher.Match(@"(sony:Sony)").Where((Sony sony) => sony.SerialNumber == SerialNumber).With("sony")
                .Set("sony.Price= " + "\"" + Price + "\"").ExecuteWithoutResultsAsync();
            return result;
        }
        public async Task<List<Sony>> GetMySonys(string Username)
        {
            var result = await _client.Cypher.Match(@"(renterer:Renterer)-[r:OWN]->(sony:Sony)").Where((Renterer renterer) => renterer.Username == Username)
                .Return((sony) => new
                {
                    Sony = sony.As<Sony>()
                }).ResultsAsync;
            List<Sony> list = new List<Sony>();
            foreach (var indeks in result)
            {
                list.Add(indeks.Sony);
            }
            return list;
        }
        public async Task<List<ReservationPreviewDTO>> GetReservedSonys(string UsernameRenterer)
        {
            var result = await _client.Cypher.Match(@"(renterer:Renterer {Username:" + "\"" + UsernameRenterer + "\"" + "}) -[r:OWN]->(sony:Sony) - [rel:RESERVED_BY] ->(rentee:Rentee)")
                            .Return((rel, sony, rentee) => new
                            {
                                ReservedPattern = rel.As<RenterListDTO>(),
                                Sony = sony.As<Sony>(),
                                Rentee = rentee.As<Rentee>()
                            })
                            .ResultsAsync;
            List<ReservationPreviewDTO> list = new List<ReservationPreviewDTO>();
            foreach (var indeks in result)
            {
                ReservationPreviewDTO rdto = new ReservationPreviewDTO();
                rdto.SerialNumber = indeks.Sony.SerialNumber;
                rdto.RenterList = indeks.ReservedPattern;
                rdto.UsernameRentee = indeks.Rentee.Username;
                list.Add(rdto);
            }
            return list;
        }
        public async Task<List<AvailableSonyDTO>> GetAllReserved()
        {
            var result = await _client.Cypher.Match(@"(renterer:Renterer) -[r:OWN]->(sony:Sony) - [rel:RESERVED_BY] ->(rentee:Rentee)")
                            .Return((sony, renterer) => new
                            {
                                Sony = sony.As<Sony>(),
                                Renterer = renterer.As<Rentee>()
                            })
                            .ResultsAsync;
            List<AvailableSonyDTO> list = new List<AvailableSonyDTO>();
            foreach (var indeks in result)
            {
                AvailableSonyDTO rdto = new AvailableSonyDTO();
                rdto.UsernameRenterer = indeks.Renterer.Username;
                rdto.Sony = indeks.Sony;
                list.Add(rdto);
            }
            return list;
        }
        public async Task<List<AvailableSonyDTO>> GetAvailableSonys()
        {
            var result = await _client.Cypher.Match(@"(sony:Sony)<-[r:OWN]-(renterer:Renterer)")
                .Return((renterer, sony) => new
                {
                    Renterer = renterer.As<Renterer>(),
                    Sony = sony.As<Sony>()
                })
                .ResultsAsync;
            List<AvailableSonyDTO> list = new List<AvailableSonyDTO>();
            foreach (var indeks in result)
            {
                AvailableSonyDTO rdto = new AvailableSonyDTO();
                rdto.Sony = indeks.Sony;
                rdto.UsernameRenterer = indeks.Renterer.Username;
                list.Add(rdto);
            }
            List<AvailableSonyDTO> rentedSonyList = await this.GetAllReserved();
            List<AvailableSonyDTO> avalSony = new List<AvailableSonyDTO>();
            bool helpFlag;
            list.ForEach(el =>
            {
                helpFlag = false;
                rentedSonyList.ForEach(e =>
                {
                    if (el.Sony.SerialNumber == e.Sony.SerialNumber)
                    { helpFlag = true; }
                });
                if (helpFlag == false) { avalSony.Add(el); }
            });
            return avalSony;
        }
        public async Task<Sony> GetSonyBySerialNumber(string SerialNumber)
        {
            var result = await _client.Cypher.Match(@"(sony:Sony)").Where((Sony sony) => sony.SerialNumber == SerialNumber)
                .Return(sony => new { Sony = sony.As<Sony>() }).ResultsAsync;
            Sony sony = new Sony();
            foreach (var indeks in result)
            {
                sony = indeks.Sony;
            }
            return sony;
        }

        public async Task<Sony> ReservedMySony(string SerialNumber, string UsernameRentee, RenterListDTO renterList)
        {
            bool validation = await this.IsSonyFree(SerialNumber, UsernameRentee);
            if (validation == true)
            {
                var result = await _client.Cypher.Match(@"(sony:Sony)").Where((Sony sony) => sony.SerialNumber == SerialNumber)
              .Match(@"(rentee:Rentee)").Where((Rentee rentee) => rentee.Username == UsernameRentee)
              .Create("(sony) - [r:RESERVED_BY {renterList}]-> (rentee)").WithParams(new { renterList })
              .Return((sony) => new { Sony = sony.As<Sony>() }).ResultsAsync;
                foreach (var indeks in result)
                {
                    return indeks.Sony;
                }
            }
            return null;
        }

        public async Task<bool> IsSonyFree(string SerialNumber, string UsernameRentee)
        {
            Sony sony = new Sony();
            var result = await _client.Cypher
                .Match(@"(sony:Sony {SerialNumber:" + "\"" + SerialNumber + "\"" + "})-[r:RESERVED_BY]->(rentee:Rentee {Username:" + "\"" + UsernameRentee + "\"" + "})")
                .Return((sony) => new
                {
                    Sony = sony.As<Sony>()
                })
                .ResultsAsync;
            foreach (var indeks in result)
            {
                if (indeks.Sony != null)
                    sony = indeks.Sony;
                else return false;
            }
            return true;
        }
    }
}
