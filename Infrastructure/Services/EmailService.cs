using Domain.DTOs;

namespace Infrastructure.Services;

using MailKit.Net.Smtp;
using MimeKit;

public class EmailService
{
    private readonly EmailConfiguration _emailConfig;

    public EmailService(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public async Task SendEmailAsync(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromAddress));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, true);
        await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
        await client.SendAsync(mailMessage);
        await client.DisconnectAsync(true);
    }
}