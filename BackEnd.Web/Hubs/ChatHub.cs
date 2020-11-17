using Microsoft.AspNetCore.SignalR;      // using this
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Hubs
{
  public class ChatHub : Hub      // inherit this
  {
    
    public Task SendMessage1(string connectionId, string user, string message)               // Two parameters accepted
        {
            return Clients.Client(connectionId).SendAsync("ReceiveOne", user, message);    // Note this 'ReceiveOne' 
        }

    public chatUsers getConnectionUserList()
        {
        chatUsers chatUsersList = chatUsers.Instance;
        var user = new User();
        user.connectionId = Context.ConnectionId;
        chatUsersList.Add(user);
          return chatUsersList;
        }

    public override async Task OnConnectedAsync() {
      await Clients.All.SendAsync("UserConnected",Context.ConnectionId);
      await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception Ex)
    {
      await Clients.All.SendAsync("UserDisConnected", Context.ConnectionId);
      await base.OnDisconnectedAsync(Ex);
    }

  }
}
