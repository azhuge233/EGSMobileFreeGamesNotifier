using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class Bark(ILogger<Bark> logger, IOptions<Config> config) : INotifiable {
        private readonly ILogger<Bark> _logger = logger;
        private readonly Config config = config.Value;

        public async Task SendMessage(List<NotifyRecord> records) {
            try {
                var sb = new StringBuilder();
                string url = sb.AppendFormat(NotifyFormatStrings.barkUrlFormat, config.BarkAddress, config.BarkToken).ToString();


                var client = new HttpClient();

                foreach (var record in records) {
                    sb.Clear();
                    _logger.LogDebug($"{NotifierStrings.debugSendMessageBark}: {record.Title}");

                    await client.GetAsync(
                        sb.Append(url)
                        .Append(NotifyFormatStrings.barkUrlTitle)
                        .Append(HttpUtility.UrlEncode(record.ToBarkMessage()))
                        .Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
                        .AppendFormat(NotifyFormatStrings.barkUrlArgs, HttpUtility.UrlEncode(record.UrlAndroid))
                        .ToString()
                    );

                    _logger.LogDebug(sb.ToString());
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageBark}");
            } catch (Exception) {
                _logger.LogDebug($"Error: {NotifierStrings.debugSendMessageBark}");
                throw;
            } finally {
                Dispose();
            }
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
