using System.Text;
using System.Text.Json;
using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.PostContent;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class QQHttp(ILogger<QQHttp> logger) : INotifiable {
        private readonly ILogger<QQHttp> _logger = logger;

        public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
            try {
                _logger.LogDebug(NotifierStrings.debugSendMessageQQHttp);

                string url = string.Format(NotifyFormatStrings.qqHttpUrlFormat, config.QQHttpAddress, config.QQHttpPort, config.QQHttpToken);

                var client = new HttpClient();

                var content = new QQHttpPostContent {
                    UserID = config.ToQQID
                };

                var data = new StringContent(string.Empty);
                var resp = new HttpResponseMessage();

                foreach (var record in records) {
                    _logger.LogDebug($"{NotifierStrings.debugSendMessageQQHttp} : {record.Title}");

                    content.Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}";

                    data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    resp = await client.PostAsync(url, data);

                    _logger.LogDebug(await resp.Content.ReadAsStringAsync());
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQHttp}");
            } catch (Exception) {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessageQQHttp}");
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
