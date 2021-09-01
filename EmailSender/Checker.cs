using MailKit.Net.Imap;
using MailKit;
using System;
using System.Collections.Generic;

namespace EmailSender {
    public class Checker {
        public Checker(){
            
        }
        public static IList<IMessageSummary> GetMailFolder(){
            IList<IMessageSummary> inbox;
            using (var client = new ImapClient()){
                client.Connect(Configuration["EmailSmtp"], 993, true);
                client.Authenticate(Configuration["EmailFrom"], Configuration["EmailFromAccountPassword"]);
                client.Inbox.Open(FolderAccess.ReadOnly);
                inbox = client.Inbox.Fetch(0, -1, MessageSummaryItems.Full);
                client.Disconnect(true);
            }
            return inbox;
        }
        public static IMailFolder OpenInbox(){
            IMailFolder inbox;
            using (var client = new ImapClient()){
                client.Connect(Configuration["EmailServerSmtp"], 993, true);
                client.Authenticate(Configuration["EmailFromAddress"], Configuration["EmailFromAccountPassword"]);
                inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                Console.WriteLine($"Total Messages: {inbox.Count}");
                Console.WriteLine($"Recent Messages: {inbox.Recent}");
                for(var i = 0; i < inbox.Count; i++){
                    Console.WriteLine($"Subject: {inbox.GetMessage(i).Subject}");
                }
                client.Disconnect(true);
            }
            return inbox;
        }

    }
}