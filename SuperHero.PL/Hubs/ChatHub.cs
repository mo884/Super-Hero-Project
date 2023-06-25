using Microsoft.AspNetCore.SignalR;

namespace SuperHero.PL.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message,string Path,string ID)
        {
           
            await Clients.All.SendAsync("ReceiveMessage", user, message, Path, ID);
        }

        public async Task JoinGroup(string group, string name)
        {
            await Clients.All.SendAsync("GroupMessage", name, group);
        }

        public async Task SendToGroup(string group, string name, string message)
        {
            await Clients.All.SendAsync("GroupSendToMessage", name, group, message);
        }

    }
}
