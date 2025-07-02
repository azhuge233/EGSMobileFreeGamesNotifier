using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.PostContent;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class Discord(ILogger<Discord> logger, IOptions<Config> config) : INotifiable {
        private readonly ILogger<Discord> _logger = logger;
        private readonly Config config = config.Value;

        private readonly int DiscordMaxEmbedCount = 10;
        public async Task SendMessage(List<NotifyRecord> records) {
            try {
                _logger.LogDebug(NotifierStrings.debugSendMessageDiscord);

                var url = config.DiscordWebhookURL;

                var client = new HttpClient();

                for (int i = 0; i <= records.Count / DiscordMaxEmbedCount; i++) {
                    var content = new DiscordPostContent() {
                        Content = records.Count > 1 ? NotifyFormatStrings.discordTitleNewFreeGames : NotifyFormatStrings.discordTitleNewFreeGame
                    };

                    for (int j = i * DiscordMaxEmbedCount; (j - i * DiscordMaxEmbedCount) < 10 && j < records.Count; j++) {
                        content.Embeds.Add(
                            new Embed() {
                                Title = records[j].Title,
                                Url = string.Empty, // records[j].UrlAndroid,
                                Description = records[j].ToDiscordMessage(),
                                Footer = new Footer() { Text = NotifyFormatStrings.projectLink }
                            }
                        );
                    }

                    if (content.Embeds.Count > 0) {
                        var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                        var resp = await client.PostAsync(url, data);

                        _logger.LogDebug(await resp.Content.ReadAsStringAsync());
                    }
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageDiscord}");
            } catch (Exception) {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessageDiscord}");
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
