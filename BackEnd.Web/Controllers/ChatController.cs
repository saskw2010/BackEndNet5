using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL.Models;
using BackEnd.Web.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace BackEnd.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
       // private readonly IHubContext<ChatHub> _hubContext;
    private IHubContext<ChatHub> _hubContext { get; set; }

    public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("send")]                                           //path looks like this: https://localhost:44379/api/chat/send
        [HttpPost]
        public IActionResult SendRequest([FromBody] MessageDto msg)
        {
            _hubContext.Clients.All.SendAsync("ReceiveOne", msg.user, msg.msgText);

            //_hubContext.Clients.Client(msg.userId).SendAsync("ReceiveOne", msg.user, msg.msgText);
            return Ok();
        }
     
  }
}
