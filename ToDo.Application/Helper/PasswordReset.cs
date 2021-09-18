﻿using System.Net.Mail;

namespace ToDo.Application.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetEmail(string link, string email)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("todo.app.confirm@gmail.com");
            mail.To.Add(email);

            mail.Subject = $"Şifre Yenileme";
            mail.Body = "<h2>Şifre yenilemek için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>Şifre Yenileme Linki</a>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("todo.app.confirm@gmail.com", "123456Aa+");

            smtpClient.Send(mail);
        }
    }
}
