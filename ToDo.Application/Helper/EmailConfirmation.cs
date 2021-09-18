using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Helper
{
    public static class EmailConfirmation
    {
        public static void SendEmail(string link, string email)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("todo.app.confirm@gmail.com");
            mail.To.Add(email);
            
            mail.Subject = $"Email doğrulama";
            mail.Body = "<h2>Email adresinizi doğrulamak için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>email doğrulama linki</a>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("todo.app.confirm@gmail.com", "123456Aa+");

            smtpClient.Send(mail);
        }
    }
}
