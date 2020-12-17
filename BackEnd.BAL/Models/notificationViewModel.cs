using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class NotificationViewModel
  {
    public string FirebaseApplicationID { get; set; }
    public string FirebaseSenderId { get; set; }
    public string englishMessage { get; set; }
    public string title { get; set; }
    public List<string> player_Id { get; set; }
    public Dictionary<string, object> AdditionalData { get; set; }
    public int second { get; set; }
  }
}
