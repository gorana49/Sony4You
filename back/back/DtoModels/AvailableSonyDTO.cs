using back.Models;
namespace back.DtoModels
{
    public class AvailableSonyDTO
    {
        public string UsernameRenterer { get; set; }
        public Sony Sony { get; set; }

        public AvailableSonyDTO()
        {
            this.Sony = new Sony();
        }
    }
}
