using Domain.Notifications;
using Infrastructure.Notifications.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Notifications;

public class EmailNotification: INotification
{
    private readonly IConfiguration _configuration;
    private readonly SmtpSettings _smtpSettings;
    
    public EmailNotification(IConfiguration configuration)
    {
        _configuration = configuration;
        _smtpSettings = new SmtpSettings
        {
            Host = _configuration["SmtpSettings:Host"],
            Port = int.Parse(_configuration["SmtpSettings:Port"]),
            Username = _configuration["SmtpSettings:Username"],
            Password = _configuration["SmtpSettings:Password"],
            MailboxName = _configuration["SmtpSettings:MailboxName"],
            From = _configuration["SmtpSettings:From"]
        };

        var val = _smtpSettings;
    }
    public async Task Send(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_smtpSettings.MailboxName, _smtpSettings.From));
        message.To.Add(MailboxAddress.Parse(to));
        
        message.Subject = subject;
        message.Body = new TextPart("html"){Text = body};

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}