using back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.IRepository
{
    public interface ICommentRepository
    {
        public Task AddCommentToRentee(Comment comm, string Username, string UsernameRenterer);
        public Task AddCommentToRenterer(Comment comm, string Username, string UsernameRentee);
        public Task DeleteComment(string title);
        public Task<List<Comment>> GetCommentRentee(string UsernameRentee, string UsernameRenterer);
        public Task UpdateComment(string Text, string Title);
    }
}
