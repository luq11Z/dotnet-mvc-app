using LStudies.Business.Notifications;
using System.Collections.Generic;

namespace LStudies.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
