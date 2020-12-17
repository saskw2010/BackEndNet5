using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace EmailService
{
    public class EmailSender : IEmailSender
    {
      
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendAsyncWithEmailAndPassword(Message message, string email,string password)
        {
            
                var mailMessage = CreateEmailMessageWithFromEmail(message, email);

                await SendAsyncWithEmailAndPassword(mailMessage,email,password);
           
        }

          public async Task SendAsyncWithEmailAndPasswordAndHtmlPage(Message message, string email,string password)
        {
            
                var mailMessage = CreateEmailMessageWithFromEmailAndHtmlPage(message, email);

                await SendAsyncWithEmailAndPassword(mailMessage,email,password);
           
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }


        public async Task SendEmailMobileAsync(Message message)
        {
            var mailMessage = CreateMobileEmailMessage(message);
            await SendAsync(mailMessage);
        }

    private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<div><h1 style='color:#0678F4'>Hello from www.WytSky.com </h1>" +
              "<p>press on Url or whrite this code </p>" +
              "<p>{0}</p></div>", message.Content) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

    private MimeMessage CreateMobileEmailMessage(Message message)
    {
      var emailMessage = new MimeMessage();
      emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
      emailMessage.To.AddRange(message.To);
      emailMessage.Subject = message.Subject;

      var bodyBuilder = new BodyBuilder
      {
        HtmlBody = string.Format("<div><h1 style='color:#0678F4'>Hello from www.WytSky.com </h1>" +
        "<p>{0}</p></div>", message.Content)
      };

      if (message.Attachments != null && message.Attachments.Any())
      {
        byte[] fileBytes;
        foreach (var attachment in message.Attachments)
        {
          using (var ms = new MemoryStream())
          {
            attachment.CopyTo(ms);
            fileBytes = ms.ToArray();
          }

          bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
        }
      }

      emailMessage.Body = bodyBuilder.ToMessageBody();
      return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    // client.AuthenticationMechanisms.Remove("XOAUTH2");
                    // client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    // client.Send(mailMessage);

                      client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);

                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                        client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
        try
        {
          // await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
          // client.AuthenticationMechanisms.Remove("XOAUTH2");
          // await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

          // await client.SendAsync(mailMessage);
          client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                     await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);

                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                        await client.SendAsync(mailMessage);



      }
                catch (Exception ex)
      {
        //log an error message or throw an exception, or both.
        throw ex;
      }
      finally
      {
        await client.DisconnectAsync(true);
        client.Dispose();
      }
    }
        }

        private async Task SendAsyncWithEmailAndPassword(MimeMessage mailMessage,string email,string password)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    

                    // await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    // client.AuthenticationMechanisms.Remove("XOAUTH2");
                    // await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    // await client.SendAsync(mailMessage);
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                   

                     await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                     

                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        client.Authenticate(email, password);
                        await client.SendAsync(mailMessage);



                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception, or both.
                    throw ex;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailMessageWithFromEmail(Message message,string email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(email));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            //file bytes

            foreach (var attachment in message.InformationFiles) {
                bodyBuilder.Attachments.Add(attachment.fileName, attachment.Bytes);
            }
                

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

         private MimeMessage CreateEmailMessageWithFromEmailAndHtmlPage(Message message,string email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(email));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2> <div>{1}</div>", message.Content,message.ContentHtml) };

            //file bytes

            foreach (var attachment in message.InformationFiles) {
                bodyBuilder.Attachments.Add(attachment.fileName, attachment.Bytes);
            }
                

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

  }

    

     
}
