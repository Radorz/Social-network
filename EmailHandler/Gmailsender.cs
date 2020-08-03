using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmailHandler
{
   public class Gmailsender : Iemailsender
    {

        private readonly EMconfi _EMconfi;
        public Gmailsender(EMconfi EMconfi)
        {
            _EMconfi = EMconfi;

        }
        public async Task SendMailAsync(Message message)
        {
            var emailmessage = CreateEmailMessage(message);
            await SendAsync(emailmessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
           
            var emailmessage = new MimeMessage();
            emailmessage.From.Add(new MailboxAddress(_EMconfi.From));
            emailmessage.To.AddRange(message.To);
            emailmessage.Subject = message.Subject;
            emailmessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailmessage;

        } 
        private async Task SendAsync (MimeMessage mimeMessage)
        {

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_EMconfi.SmtpServer, _EMconfi.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_EMconfi.UserName, _EMconfi.Password);
                    await client.SendAsync(mimeMessage);

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {

                    await client.DisconnectAsync(true);
                }



            }

        }
    }
}
