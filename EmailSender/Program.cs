using System;
using System.Net.Mail;
namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = new MailMessage(
                new MailAddress("joey@joeyguerra.com", "Joey G"),
                new MailAddress("test@rockithelpdesk.com", "Test McTester")
            );
            Sender.Send(message);
            Checker.GetMailFolder();
        }
    }
}
