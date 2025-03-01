namespace Domain.Notifications;

public interface INotification
{
    Task Send(string to, string subject, string body);
}