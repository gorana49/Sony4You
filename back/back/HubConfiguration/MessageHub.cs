using back.DtoModels;
using Microsoft.AspNetCore.SignalR;

namespace back.HubConfiguration
{
    public class MessageHub : Hub
    {
        //Dictionary<string, string> onlineUsers = new Dictionary<string, string>();
        public void NewMessage(MessageDTO mess)
        {
            // Console.WriteLine(mess.receiverId, mess.clientuniqueid);
            //   onlineUsers.Add(mess.senderUsername, mess.clientuniqueid);
            //    Clients.Client(mess.receiverId).SendAsync("MessageReceived", mess);

            Clients.All.SendAsync("MessageReceived", mess);

        }
        //public string GetConnectionId(string Username)
        //{
        //    onlineUsers.Add(Username, Context.ConnectionId);
        //    Console.WriteLine(onlineUsers.Keys);
        //    return Context.ConnectionId;
        //}
        //  public string GetConnectionId(string Username) => Context.ConnectionId;
    }
}
