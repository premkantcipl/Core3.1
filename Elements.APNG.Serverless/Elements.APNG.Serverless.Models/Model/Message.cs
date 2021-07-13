using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace Elements.APNG.Serverless.Models.Model
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        private readonly List<Attachment> _attachments;
        public IEnumerable<Attachment> Attachments { get { return _attachments; } }
        public string PlainContent { get; set; }

        public Message(string to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.Add(new MailboxAddress(to));
            Subject = subject;
            Content = content;
            _attachments = new List<Attachment>();
        }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            _attachments = new List<Attachment>();
        }

        public void Attach(Attachment attachment) 
        {
            _attachments.Add(attachment);
        }
    }
}