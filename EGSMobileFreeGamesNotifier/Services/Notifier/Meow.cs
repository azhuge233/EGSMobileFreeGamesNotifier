using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.PostContent;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class Meow(ILogger<Meow> logger, IOptions<Config> config) : INotifiable {
        private readonly ILogger<Meow> _logger = logger;
        private readonly Config config = config.Value;

		public async Task SendMessage(List<NotifyRecord> records) {
            try {
                _logger.LogDebug(NotifierStrings.debugSendMessageMeow);

                var url = string.Format(NotifyFormatStrings.meowUrlFormat, config.MeowAddress, config.MeowNickname);

                var content = new MeowPostContent() {
                    Title = NotifyFormatStrings.meowUrlTitle
                };

                var client = new HttpClient();

                foreach (var record in records) {
                    content.Message = $"{record.ToMeowMessage()}{NotifyFormatStrings.projectLink}";
                    // content.Url = record.Url;

                    var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    var resp = await new HttpClient().PostAsync(url, data);

                    _logger.LogDebug(await resp.Content.ReadAsStringAsync());
                    await Task.Delay(3000); // rate limit
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageMeow}");
            } catch (Exception) {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessageMeow}");
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
