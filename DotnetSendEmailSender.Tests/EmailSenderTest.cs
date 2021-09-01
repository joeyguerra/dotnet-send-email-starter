using Xunit;
using EmailSender;
using System.Net.Mail;
using System;
using System.Dynamic;

namespace DotnetSendEmailSender.Tests
{
    public class EmailSenderTest {
        [Fact]
        public void ShouldSendEmail(){
            dynamic model = new ExpandoObject();
            model.Today = DateTime.Now.ToString();
            var template = new EmailTemplate("Test Subject {{Today}}", model);

            var message = new MailMessage(
                new MailAddress("joey@joeyguerra.com", "Joey G"),
                new MailAddress("test@rockithelpdesk.com", "Test McTester")
            ){
                Subject = template.Content,
                Body = template.Content
            };
            Sender.Send(message);
            var inbox = Checker.GetMailFolder();
            // foreach (var summary in inbox) {
            //     Console.WriteLine ("[summary] {0:D2}: {1}", summary.Index, summary.Envelope.Subject);
            // }
            var lastOne = inbox[inbox.Count-1];
            Assert.Contains(model.Today.ToString(), lastOne.Envelope.Subject);
        }

        [Fact]
        public void EmailTemplateShouldLookGood(){
            dynamic model = new ExpandoObject();
            model.Name = "joey";
            var template = new EmailTemplate("{{Name}}", model);
            Assert.Contains(model.Name, template.Content);
        }
    }
}