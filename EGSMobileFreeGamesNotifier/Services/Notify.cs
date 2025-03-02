using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Modules;
using EGSMobileFreeGamesNotifier.Services.Notifier;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Notify(ILogger<Notify> logger) : IDisposable {
        private readonly ILogger<Notify> _logger = logger;
        private readonly IServiceProvider services = DI.BuildDiNotifierOnly();

        public async Task DoNotify(NotifyConfig config, List<NotifyRecord> pushList) {
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
                    notifyTasks.Add(services.GetRequiredService<TelegramBot>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Telegram");

                // Bark notifications
                if (config.EnableBark) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Bark");
                    notifyTasks.Add(services.GetRequiredService<Bark>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Bark");

				// QQ Http notifications
				if (config.EnableQQHttp) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "QQ Http");
                    notifyTasks.Add(services.GetRequiredService<QQHttp>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "QQ Http");

				// QQ WebSocket notifications
				if (config.EnableQQWebSocket) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "QQ WebSocket");
                    notifyTasks.Add(services.GetRequiredService<QQWebSocket>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "QQ WebSocket");

                // PushPlus notifications
                if (config.EnablePushPlus) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "PushPlus");
                    notifyTasks.Add(services.GetRequiredService<PushPlus>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "PushPlus");

                // DingTalk notifications
                if (config.EnableDingTalk) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "DingTalk");
                    notifyTasks.Add(services.GetRequiredService<DingTalk>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "DingTalk");

                // PushDeer notifications
                if (config.EnablePushDeer) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "PushDeer");
                    notifyTasks.Add(services.GetRequiredService<PushDeer>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "PushDeer");

                // Discord notifications
                if (config.EnableDiscord) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Discord");
                    notifyTasks.Add(services.GetRequiredService<Discord>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Discord");

                // Email notifications
                if (config.EnableEmail) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Email");
                    notifyTasks.Add(services.GetRequiredService<Email>().SendMessage(config, pushList));
                } else _logger.LogInformation(NotifyStrings.debugDisabledFormat, "Email");

                // Meow notifications
                if (config.EnableMeow) {
                    _logger.LogInformation(NotifyStrings.debugEnabledFormat, "Meow");
                    notifyTasks.Add(services.GetRequiredService<Meow>().SendMessage(config, pushList));
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
