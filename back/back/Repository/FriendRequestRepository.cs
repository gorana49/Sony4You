using back.IRepository;
using Neo4jClient;
using System.Threading.Tasks;

namespace back.Repository
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly IBoltGraphClient _client;
        public FriendRequestRepository(IBoltGraphClient client)
        {
            _client = client;
        }
        public Task AddRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            var result = _client.Cypher.Match(@"(sender)").Where("(sender.Username = $SenderRequestUsername)")
                .WithParam("SenderRequestUsername", SenderRequestUsername)
                .Match(@"(receiver)")
                .Where("(receiver.Username = $ReceiverRequestUsername)")
                .WithParam("ReceiverRequestUsername", ReceiverRequestUsername)
                .Create("(sender)-[r:FRIEND_REQUEST] -> (receiver)")
                .ExecuteWithoutResultsAsync();
            return result;
        }

        public Task DeleteRequest(string SenderRequestUsername, string ReceiverRequestUsername)
        {
            var result = _client.Cypher.Match(@"(sender) - [r:FRIEND_REQUEST]-> (receiver)")
                .Where("(sender.Username = $SenderRequestUsername)")
                .WithParam("SenderRequestUsername", SenderRequestUsername)
                .AndWhere("(receiver.Username = $ReceiverRequestUsername)")
                .WithParam("ReceiverRequestUsername", ReceiverRequestUsername)
                .DetachDelete("r")
                .ExecuteWithoutResultsAsync();
            return result;
        }
        public Task MakeUsFriends(string SenderUsername, string ReceiverUsername)
        {
            var result = _client.Cypher.Match(@"(sender), (receiver)").Where("(sender.Username = $SenderRequestUsername)")
               .WithParam("SenderRequestUsername", SenderUsername)
               .AndWhere("(receiver.Username = $ReceiverRequestUsername)").WithParam("ReceiverRequestUsername", ReceiverUsername)
                .Create("(sender)-[r:FRIENDS] -> (receiver)")
                .ExecuteWithoutResultsAsync();
            return result;
        }

    }
}
