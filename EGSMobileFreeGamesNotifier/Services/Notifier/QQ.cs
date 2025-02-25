using System.Text;
using System.Web;
using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services.Notifier
{
    internal class QQ(ILogger<QQ> logger) : INotifiable
    {
        private readonly ILogger<QQ> _logger = logger;

        public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records)
        {
            try
            {
                _logger.LogDebug(NotifierStrings.debugSendMessageQQ);

                string url = new StringBuilder().AppendFormat(NotifyFormatStrings.qqUrlFormat, config.QQAddress, config.QQPort, config.ToQQID).ToString();

                var sb = new StringBuilder();
                var client = new HttpClient();

                foreach (var record in records)
                {
                    _logger.LogDebug($"{NotifierStrings.debugSendMessageQQ} : {record.Title}");

                    await client.GetAsync(
                        new StringBuilder()
                            .Append(url)
                            .Append(HttpUtility.UrlEncode(record.ToQQMessage()))
                            .Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
                            .ToString()
                    );
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQ}");
            }
            catch (Exception)
            {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessageQQ}");
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
