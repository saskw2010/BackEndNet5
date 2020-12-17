using BackEnd.BAL.Models;
using BackEnd.Service.IService;
using EmailService;
using System;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class emailService : IemailService
  {
    private readonly IEmailSender _emailSender;

    public emailService(IEmailSender emailSender)
    {
      _emailSender = emailSender;
    }
    public async Task<bool> sendVerfication(int verficationCode,string Email)
    {
      string Url = "http://localhost:4200/verfication/" + verficationCode.ToString()+"?Email="+ Email;
      Url = Url + "<p style='color:#0678F4'>" + "Verfication Code : "+verficationCode.ToString() + "</p>";
      var message = new Message(new string[] { Email }, "www.wytSky.com", Url);
      await _emailSender.SendEmailAsync(message);

      return true;
    }

    public async Task<bool> sendVerficationMobile(int verficationCode, string Email)
    {
      string Url = "<h3 style='color:#0678F4'>" + "Verfication Code : " + verficationCode.ToString() + "</h3>";
      var message = new Message(new string[] { Email }, "www.wytSky.com", Url);
      await _emailSender.SendEmailMobileAsync(message);

      return true;
    }
  }
}
