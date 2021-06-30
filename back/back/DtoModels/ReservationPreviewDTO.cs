namespace back.DtoModels
{
    public class ReservationPreviewDTO
    {
        public string SerialNumber { get; set; }
        public string UsernameRentee { get; set; }
        public RenterListDTO RenterList { get; set; }

        public ReservationPreviewDTO()
        {
            this.RenterList = new RenterListDTO();
        }
    }
}
