using Microsoft.AspNetCore.SignalR;

namespace API.Data
{
    public class MyHub : Hub
    {
        public async Task Send(string from, string to, string content)
        {
            await Clients.All.SendAsync("Receive", new SignalMessage(from, to, content));
        }

        public async Task TellNewContact(string from, string server)
        {
            await Clients.All.SendAsync("NewContact", new NewContactItem(from, server));
        }
    }
}
