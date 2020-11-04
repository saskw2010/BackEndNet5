using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
        Task SendAsyncWithEmailAndPassword(Message message, string email,string password);
        //SendAsyncWithEmailAndPasswordAndHtmlPage
        Task SendAsyncWithEmailAndPasswordAndHtmlPage(Message message, string email,string password);
    }
}