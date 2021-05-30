namespace back.DtoModels
{
    public class FriendRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public Request sender { get; set; }
    }
}
