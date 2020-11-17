using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Hubs
{
  public class User
  {
    public string connectionId { get; set; }
    public string userName { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
  }
  public class chatUsers : List<User>
  {
    private readonly static chatUsers instance = new chatUsers();

    private chatUsers() { }

    public static chatUsers Instance
    {
      get
      {
        return instance;
      }
    }
  }
}
