using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Services.Notifier;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Notify(ILogger<Notify> logger, IOptions<Config> config, TelegramBot tgBot, Bark bark, QQHttp qqHttp, QQWebSocket qqWS, PushPlus pushPlus, DingTalk dingTalk, PushDeer pushDeer, Discord discord, Email email, Meow meow) : IDisposable {
        private readonly ILogger<Notify> _logger = logger;
        private readonly Config config = config.Value;

        public async Task DoNotify(List<NotifyRecord> pushList) {
            if (pushList.Count == 0) {
                _logger.LogInformation(NotifyStrings.debugNoNewNotifications);
                return;
            }

            try {
                _logger.LogDebug(NotifyStrings.debugNotify);

                var notifyTasks = new List<Task>();

                // Telegram notifications
                if (config.EnableTelegram) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Telegram");
                    notifyTasks.Add(tgBot.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Telegram");

                // Bark notifications
                if (config.EnableBark) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Bark");
                    notifyTasks.Add(bark.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Bark");

				// QQ Http notifications
				if (config.EnableQQHttp) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "QQ Http");
                    notifyTasks.Add(qqHttp.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "QQ Http");

				// QQ WebSocket notifications
				if (config.EnableQQWebSocket) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "QQ WebSocket");
                    notifyTasks.Add(qqWS.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "QQ WebSocket");

                // PushPlus notifications
                if (config.EnablePushPlus) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "PushPlus");
                    notifyTasks.Add(pushPlus.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "PushPlus");

                // DingTalk notifications
                if (config.EnableDingTalk) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "DingTalk");
                    notifyTasks.Add(dingTalk.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "DingTalk");

                // PushDeer notifications
                if (config.EnablePushDeer) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "PushDeer");
                    notifyTasks.Add(pushDeer.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "PushDeer");

                // Discord notifications
                if (config.EnableDiscord) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Discord");
                    notifyTasks.Add(discord.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Discord");

                // Email notifications
                if (config.EnableEmail) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Email");
                    notifyTasks.Add(email.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Email");

                // Meow notifications
                if (config.EnableMeow) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Meow");
                    notifyTasks.Add(meow.SendMessage(pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Meow");

                await Task.WhenAll(notifyTasks);

                _logger.LogDebug($"Done: {NotifyStrings.debugNotify}");
            } catch (Exception) {
                _logger.LogError($"Error: {NotifyStrings.debugNotify}");
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
