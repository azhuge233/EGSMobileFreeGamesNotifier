using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.PostContent;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class PushPlus(ILogger<PushPlus> logger, IOptions<Config> config) : INotifiable {
        private readonly ILogger<PushPlus> _logger = logger;
        private readonly Config config = config.Value;

        public async Task SendMessage(List<NotifyRecord> records) {
            try {
                _logger.LogDebug(NotifierStrings.debugSendMessagePushPlus);

                var client = new HttpClient();

                var title = string.Format(NotifyFormatStrings.pushPlusTitleFormat, records.Count);

                var postContent = new PushPlusPostContent() {
                    Token = config.PushPlusToken,
                    Title = title,
                    Content = CreateMessage(records)
                };

                var data = new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(NotifyFormatStrings.pushPlusPostUrl, data);

                _logger.LogDebug(await resp.Content.ReadAsStringAsync());

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessagePushPlus}");
            } catch (Exception) {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessagePushPlus}");
                throw;
            } finally {
                Dispose();
            }
        }

        private string CreateMessage(List<NotifyRecord> records) {
            try {
                _logger.LogDebug(NotifierStrings.debugCreateMessage);

                var sb = new StringBuilder();

                records.ForEach(record => sb.AppendFormat(NotifyFormatStrings.pushPlusBodyFormat, record.ToPushPlusMessage()));

                sb.Append(NotifyFormatStrings.projectLinkHTML);

                _logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
                return sb.ToString();
            } catch (Exception) {
                _logger.LogError($"Error: {NotifierStrings.debugCreateMessage}");
                throw;
            }
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
