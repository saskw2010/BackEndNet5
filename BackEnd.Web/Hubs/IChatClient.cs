using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Hubs
{
  public interface IChatClient
  {
    Task DoSomething(int id);

    Task NotificationUpdate(int id, string message);
  }
}
