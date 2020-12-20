using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class NotificationViewModel
  {
    public string FirebaseApplicationID { get; set; }
    public string FirebaseSenderId { get; set; }
    public string Message { get; set; }
    public string Title { get; set; }
    public List<string> PlayerId { get; set; }
    public Dictionary<string, object> AdditionalData { get; set; }
    public string Second { get; set; } = "default";
	public int Badge { get; set; } = 1;
  }
}
