using BackEnd.BAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  public class notificationController : ControllerBase
  {
    [HttpPost("PushNotificationByFirebase")]
    public  void PushNotificationByFirebase([FromBody]NotificationViewModel notificationViewModel)
    {
      try
      {
        if (notificationViewModel.AdditionalData == null)
        {
          notificationViewModel.AdditionalData = new Dictionary<string, object>()
                    {
                        { "message" , notificationViewModel.englishMessage },
                        { "other_key" , true },
                        { "title" , notificationViewModel.title },
                        { "body", notificationViewModel.englishMessage },
                        { "badge" , 1 },
                        { "sound" ,"default" },
                        { "content_available" , true },
                        { "timestamp" , DateTime.UtcNow.AddHours(2).ToString() }
                    };
        }
        else
        {
          notificationViewModel.AdditionalData.Add("message", notificationViewModel.englishMessage);
          notificationViewModel.AdditionalData.Add("other_key", true);
          notificationViewModel.AdditionalData.Add("title", notificationViewModel.title);
          notificationViewModel.AdditionalData.Add("body", notificationViewModel.englishMessage);
          notificationViewModel.AdditionalData.Add("badge", 1);
          notificationViewModel.AdditionalData.Add("sound", "default");
          notificationViewModel.AdditionalData.Add("content_available", true);
          notificationViewModel.AdditionalData.Add("timestamp", DateTime.UtcNow.AddHours(2).ToString());
        }
        notificationViewModel.player_Id = notificationViewModel.player_Id.Where(pId => pId != null && pId.Length > 9).ToList();
        foreach (var deviceId in notificationViewModel.player_Id)
        {
          try
          {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            notificationViewModel.AdditionalData.Add("userToken", deviceId);
            var data = new
            {
              to = deviceId,
              priority = "high",
              content_available = true,
              notification = new
              {
                body = notificationViewModel.englishMessage,
                title = notificationViewModel.title,
                badge = 1,
                sound = "default",
                content_available = true
              },
              data = notificationViewModel.AdditionalData,
              apns = new
              {
                payload = new
                {
                  aps = new
                  {
                    sound = "default",
                    content_available = true,
                    body = notificationViewModel.englishMessage,
                    message = notificationViewModel.englishMessage,
                    title = notificationViewModel.title,
                    badge = 1,
                  },
                },
                customKey = "test app",
              }

            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", notificationViewModel.FirebaseApplicationID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", notificationViewModel.FirebaseSenderId));
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
              dataStream.Write(byteArray, 0, byteArray.Length);
              using (WebResponse tResponse = tRequest.GetResponse())
              {
                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                {
                  using (StreamReader tReader = new StreamReader(dataStreamResponse))
                  {
                    String sResponseFromServer = tReader.ReadToEnd();
                    string str = sResponseFromServer;
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            string Error = string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "");
            //System.Diagnostics.Debug.WriteLine(Error);
            string tokens = "tokens is : (" + deviceId + ")";
            System.Diagnostics.Debug.WriteLine(string.Format("{0} :::: {1}", Error, tokens), DateTime.Now);
          }
        }
      }
      catch (Exception ex)
      {
        string Error = string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "");
        //System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : ""));
        string tokens = "tokens is : (";
        foreach (var item in notificationViewModel.player_Id)
        {
          tokens += "{" + item + "}   ";
        }
        tokens += "  )";
        System.Diagnostics.Debug.WriteLine(string.Format("{0} :::: {1}", Error, tokens), DateTime.Now);
      }
    }

  }
}
