using MimeKit;
using System.Collections.Generic;

namespace ChessMeters.Web.Email
{
    public class EmailMessage
    {
        public EmailMessage(MailboxAddress from, IEnumerable<MailboxAddress> to, string subject, string content)
        {
            From = from;
            To = to;
            Subject = subject;
            Content = content;
        }

        public MailboxAddress From { get; set; }

        public IEnumerable<MailboxAddress> To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
