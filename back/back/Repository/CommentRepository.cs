using back.IRepository;
using back.Models;
using Neo4jClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IBoltGraphClient _client;
        public CommentRepository(IBoltGraphClient client)
        {
            _client = client;
        }

        public Task AddCommentToRentee(Comment comm, string Username, string UsernameRenterer)
        {
            var result = _client.Cypher.Create("(comment:Comment {comm})").WithParams(new { comm })
                .With("comment")
                .Match(@"(rentee:Rentee)").Where((Rentee rentee) => rentee.Username == Username)
                .With("comment, rentee")
                .Create("(rentee)-[r:HAS { Written_by:" + "\"" + UsernameRenterer + "\"" + "}] -> (comment)")
                .ExecuteWithoutResultsAsync();
            return result;
        }
        public Task AddCommentToRenterer(Comment comm, string Username, string UsernameRentee)
        {
            var result = _client.Cypher.Create("(comment:Comment {comm})").WithParams(new { comm })
                   .With("comment")
               .Match(@"(renterer:Renterer)").Where((Renterer renterer) => renterer.Username == Username)
                  .With("comment, renterer")
               .Create("(renterer)-[r:HAS { Written_by:" + "\"" + UsernameRentee + "\"" + "}] -> (comment)")
               .ExecuteWithoutResultsAsync();
            return result;
        }

        public Task DeleteComment(string title)
        {
            var result = _client.Cypher.Match("(comment:Comment)")
               .Where((Comment comment) => comment.Title == title)
               .DetachDelete("comment")
               .ExecuteWithoutResultsAsync();
            return result;
        }

        //public Task<List<Comment>> GetCommentRentee(string Username)
        //{
        //    var result = _client.Cypher.Match(@"(rentee:Rentee)-[r:HAS]->(comment:Comment)")
        //        .Where((Rentee rentee) => rentee.Username == Username)
        //        .Return((, comment) => new { })

        //}
        public async Task<List<Comment>> GetCommentRentee(string UsernameRentee, string UsernameRenterer)
        {
            var result = await _client.Cypher.Match(@"(rentee:Rentee)-[r:HAS { Written_by:" + "\"" + UsernameRenterer + "\"" + "}] -> (comment)")
                .Where((Rentee rentee) => rentee.Username == UsernameRentee)
                .Return((comment) => new
                {
                    Comment = comment.As<Comment>()
                }).ResultsAsync;
            List<Comment> comments = new List<Comment>();
            foreach (var indeks in result)
            {
                comments.Add(indeks.Comment);
            }
            return comments;
        }

        public Task UpdateComment(string Text, string Title)
        {
            var result = _client.Cypher.Match(@"(comment:Comment)").Where((Comment comment) => comment.Title == Title)
                .Set("comment.Text" + "\"" + Text + "\"").ExecuteWithoutResultsAsync();
            return result;
        }

    }
}
