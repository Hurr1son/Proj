using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
    public class EmailService
        {
            public async Task SendEmailAsync(string email, string subject, string message)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Администрация сайта Car Maintenance", "alexwoonstep@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("alexwoonstep@gmail.com", "eceville12hurrison");
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
    //public async Task SendEmailAsync2(string email, string subject, string message)
    //{
    //    var emailMessage2 = new MimeMessage();

    //    emailMessage2.From.Add(new MailboxAddress("Администрация сайта Car Maintenance", "alexwoonstep@gmail.com"));
    //    emailMessage2.To.Add(new MailboxAddress("", "forgyusers@gmail.com"));
    //    emailMessage2.Subject = subject;
    //    emailMessage2.Body = new TextPart(MimeKit.Text.TextFormat.Html)
    //    {
    //        Text = message
    //    };

    //    using (var client = new SmtpClient())
    //    {
    //        await client.ConnectAsync("smtp.gmail.com", 587, false);
    //        await client.AuthenticateAsync("alexwoonstep@gmail.com", "eceville12hurrison");
    //        await client.SendAsync(emailMessage2);

    //        await client.DisconnectAsync(true);
    //    }
    //}
}

