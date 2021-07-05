using back.DtoModels;
using Microsoft.AspNetCore.SignalR;

namespace back.HubConfiguration
{
    public class NotificationHub : Hub
    {
        public void NewNotification(NotificationDTO notf)
        {
            // Console.WriteLine(mess.receiverId, mess.clientuniqueid);
            //   onlineUsers.Add(mess.senderUsername, mess.clientuniqueid);
            //    Clients.Client(mess.receiverId).SendAsync("MessageReceived", mess);
            Clients.All.SendAsync("NotificationReceived", notf);

        }
    }
}
