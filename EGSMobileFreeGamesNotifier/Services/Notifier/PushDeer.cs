using System.Text;
using System.Web;
using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services.Notifier
{
    internal class PushDeer(ILogger<PushDeer> logger) : INotifiable
    {
        private readonly ILogger<PushDeer> _logger = logger;

        public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records)
        {
            try
            {
                _logger.LogDebug(NotifierStrings.debugSendMessagePushDeer);

                var sb = new StringBuilder();
                var client = new HttpClient();

                foreach (var record in records)
                {
                    _logger.LogDebug($"{NotifierStrings.debugSendMessagePushDeer} : {record.Title}");

                    await client.GetAsync(
                        new StringBuilder()
                        .AppendFormat(NotifyFormatStrings.pushDeerUrlFormat,
                                    config.PushDeerToken,
                                    HttpUtility.UrlEncode(record.ToPushDeerMessage()))
                        .Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
                        .ToString()
                    );
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessagePushDeer}");
            }
            catch (Exception)
            {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessagePushDeer}");
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
