using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RR.Models.Rewards_Campaigns;

namespace RR.Models
{
    public class EmailService : IEmailTestService
    {
        public void SendAllMailId(List<string> mails, string campname, string rewardType)
        {
            foreach (var mail in mails)
            {
                sendEmail(mail, campname, rewardType);
            }
        }

        public void sendEmail(string email, string campname, string rewardType)
        {
            string fromMail = "sanjayprakash13072001@gmail.com";
            string fromPassword = "fqudyhcuthujbibu";



            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Campaign Started on "+ DateTime.Now.Date.ToString();
            message.To.Add(new MailAddress(email));
            message.Body = $"<html><h1>{campname} started of Rewardtype {rewardType}</h1><body>Campaign Started !...{DateTime.Now.Date.ToString()} </body></html>";
            message.IsBodyHtml = true;



            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };



            smtpClient.Send(message);
        }
    }
}
