using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class TelegramBot(ILogger<TelegramBot> logger, IOptions<Config> config) : INotifiable {
        private readonly ILogger _logger = logger;
        private readonly Config config = config.Value;

        public async Task SendMessage(List<NotifyRecord> records) {
            var BotClient = new TelegramBotClient(token: config.TelegramToken);

            try {
                foreach (var record in records) {
                    _logger.LogDebug($"{NotifierStrings.debugSendMessageTelegram} : {record.Title}");
                    await BotClient.SendMessage(
                        chatId: config.TelegramChatID,
                        text: $"{record.ToTelegramMessage()}{NotifyFormatStrings.projectLinkHTML.Replace("<br>", "\n")}",
                        parseMode: ParseMode.Html
                    );
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageTelegram}");
            } catch (Exception) {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessageTelegram}");
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
