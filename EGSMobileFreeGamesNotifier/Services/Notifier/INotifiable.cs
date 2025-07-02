using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;

namespace EGSMobileFreeGamesNotifier.Services.Notifier
{
    internal interface INotifiable : IDisposable
    {
        public Task SendMessage(List<NotifyRecord> records);
    }
}
