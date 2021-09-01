
using MailKit;
using MimeKit;
using System.Net.Mail;
using System;
namespace EmailSender {
    public class Sender {
        public Sender(){
            
        }
        public static void Send(MailMessage mailMessage){
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailMessage.From.DisplayName, mailMessage.From.Address));
            foreach(var to in mailMessage.To){
                message.To.Add(new MailboxAddress(to.DisplayName, to.Address));
            }

            message.Subject = mailMessage.Subject;
            message.Body = new TextPart(mailMessage.IsBodyHtml ? "html" : "plain") {
				Text = mailMessage.Body,
			};
            Console.WriteLine($"Sending message {message.Body}");
			using (var client = new MailKit.Net.Smtp.SmtpClient ()) {
                client.Connect(Configuration["EmailServerSmtp"], 465, true);
                client.Authenticate(Configuration["FromEmailAddress"], Configuration["FromEmailAccountPassword"]);
				client.Send(message);
				client.Disconnect (true);
            }
        }
    }
}