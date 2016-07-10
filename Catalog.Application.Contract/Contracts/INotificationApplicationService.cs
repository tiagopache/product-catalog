using Catalog.Application.Contract.ViewModels;
using System.Threading.Tasks;

namespace Catalog.Application.Contract.Contracts
{
    public interface INotificationApplicationService : IApplicationServiceBase<NotificationViewModel>
    {
        Task Initialize();

        Task Receive(NotificationViewModel notificationReceived);
    }
}
