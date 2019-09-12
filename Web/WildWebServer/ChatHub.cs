using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WildWebServer
{
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
