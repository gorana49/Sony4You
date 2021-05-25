using back.Models;

namespace back.DtoModels
{
    public class AddCommentDTO
    {
        public string Username { get; set; }
        public string UsernameRentee { get; set; }
        public Comment Comment { get; set; }
    }
}
