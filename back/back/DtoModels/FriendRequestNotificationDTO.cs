namespace back.DtoModels
{
    public class FriendRequestNotificationDTO
    {
        public int ReceiverId { get; set; }
        public string IdNotification { get; set; }
        public Request Request { get; set; }
    }
}
