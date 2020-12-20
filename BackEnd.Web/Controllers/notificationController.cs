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
    [HttpPost("PushFirebaseNotificationForDevices")]
    public void PushFirebaseNotificationForDevices([FromBody] NotificationViewModel notificationViewModel)
    {
      try
      {
        notificationViewModel.PlayerId = notificationViewModel.PlayerId.Where(pId => pId != null && pId.Length > 9).ToList();
        foreach (var deviceId in notificationViewModel.PlayerId)
        {
          try
          {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";

            var data = new
            {
              to = deviceId,
              priority = "high",
              content_available = true,
              notification = new
              {
                body = notificationViewModel.Message,
                title = notificationViewModel.Title,
                badge = notificationViewModel.Badge,
                sound = notificationViewModel.Second,
                content_available = true
              },
              data = notificationViewModel.AdditionalData,
              apns = new
              {
                payload = new
                {
                  aps = new
                  {
                    sound = notificationViewModel.Second,
                    content_available = true,
                    body = notificationViewModel.Message,
                    message = notificationViewModel.Message,
                    title = notificationViewModel.Title,
                    badge = notificationViewModel.Badge,
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
        foreach (var item in notificationViewModel.PlayerId)
        {
          tokens += "{" + item + "}   ";
        }
        tokens += "  )";
        System.Diagnostics.Debug.WriteLine(string.Format("{0} :::: {1}", Error, tokens), DateTime.Now);
      }
    }

    [HttpPost("PushFirebaseNotificationForAll")]
    public void PushFirebaseNotificationForAll([FromBody] NotificationViewModel notificationViewModel)
    {
      try
      {
        try
        {
          WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
          tRequest.Method = "post";
          tRequest.ContentType = "application/json";

          var data = new
          {
            to = "/topics/all",
            priority = "high",
            content_available = true,
            notification = new
            {
              body = notificationViewModel.Message,
              title = notificationViewModel.Title,
              badge = notificationViewModel.Badge,
              sound = notificationViewModel.Second,
              content_available = true
            },
            data = notificationViewModel.AdditionalData,
            apns = new
            {
              payload = new
              {
                aps = new
                {
                  sound = notificationViewModel.Second,
                  content_available = true,
                  body = notificationViewModel.Message,
                  message = notificationViewModel.Message,
                  title = notificationViewModel.Title,
                  badge = notificationViewModel.Badge,
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
          //string tokens = "tokens is : (" + deviceId + ")";
          System.Diagnostics.Debug.WriteLine(string.Format("{0} :::: ", Error), DateTime.Now);
        }
      }
      catch (Exception ex)
      {
        string Error = string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "");
        //System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : ""));
        string tokens = "tokens is : (";
        foreach (var item in notificationViewModel.PlayerId)
        {
          tokens += "{" + item + "}   ";
        }
        tokens += "  )";
        System.Diagnostics.Debug.WriteLine(string.Format("{0} :::: {1}", Error, tokens), DateTime.Now);
      }
    }


  }
}
