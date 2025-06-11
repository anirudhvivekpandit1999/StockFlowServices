using System.Net;
using System.Net.Mail;

namespace StockFlowService.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("anirudhvpandit.2152@gmail.com", "vwtf lkzn ubjx gmtx"), 
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("anirudhvpandit.2152@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            smtpClient.Send(mailMessage);
        }
    }
}
