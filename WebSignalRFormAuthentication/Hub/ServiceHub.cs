using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using WebSignalRFormAuthentication.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebSignalRFormAuthentication
{
    //[HubName("ServiceHub")]
    public class ServiceHub : Hub
    {
        [Authorize]
        public async Task ListUser()
        {
            //var userList = new List<Register>();
            //var user = Context.User;

            //if (user.Identity.IsAuthenticated)
            //{
                using (var dataContext = new DataContext())
                {
                    var userList = (from user1 in dataContext.Registers select user1).ToList();

                await Clients.Client(Context.ConnectionId).GetListMember(userList);
                }
            //}
            
        }
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}