﻿using MimeKit;

namespace Domain.DTOs;

// Models/Message.cs
public class Message
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public Message(IEnumerable<string> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(email => new MailboxAddress("", email)));
        Subject = subject;
        Content = content;
    }
}