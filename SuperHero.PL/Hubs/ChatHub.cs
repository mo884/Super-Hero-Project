using Microsoft.AspNetCore.SignalR;
using SuperHero.DAL.Database;

namespace SuperHero.PL.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationContext _context;
        public ChatHub(ApplicationContext _context)
        {
            this._context = _context;
        }
        public async Task SendMessage(string user, string message,string Path,string ID, string GroupID,string UserID)
        {
            var group = _context.Groups.Where(a => a.ID == Convert.ToInt32(GroupID)).FirstOrDefault();

            var Person = _context.Persons.Where(a => a.Id == UserID).FirstOrDefault();
            ChatGroup chatGroup = new ChatGroup() { Message = message, group = group, groupId = group.ID, Person = Person, PersonId = Person.Id };
            _context.ChatGroups.Add(chatGroup);
            _context.SaveChanges();
            await Clients.All.SendAsync("ReceiveMessage", user, message, Path, ID);
        }
        //public async Task sendMessageToUser(string ConnectionId , string Message)
        //{
        //    return await Clients.Clients(ConnectionId).SendAsync("ReceiveMessage", Message);
        //}

        public async Task JoinGroup(string group, string name)
        {
            await Clients.All.SendAsync("GroupMessage", name, group);
        }

        public async Task SendToGroup(string group, string name, string message)
        {
            await Clients.All.SendAsync("GroupSendToMessage", name, group, message);
           
        }
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
       
        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
