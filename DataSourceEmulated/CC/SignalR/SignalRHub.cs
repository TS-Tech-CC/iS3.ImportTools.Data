using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceEmulated.CC
{
    public class SignalRHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.sendMessage(message);
        }
    }
}
