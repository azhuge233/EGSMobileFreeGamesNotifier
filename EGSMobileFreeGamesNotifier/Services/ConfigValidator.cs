using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class ConfigValidator(ILogger<ConfigValidator> logger) : IDisposable {
        private readonly ILogger<ConfigValidator> _logger = logger;

        internal void CheckValid(Config config) {
            try {
                _logger.LogDebug(ConfigValidatorStrings.debugCheckValid);

                // Telegram
                if (config.EnableTelegram) {
                    if (string.IsNullOrEmpty(config.TelegramToken))
                        throw new Exception(message: ConfigValidatorStrings.noTelegramToken);
                    if (string.IsNullOrEmpty(config.TelegramChatID))
                        throw new Exception(message: ConfigValidatorStrings.noTelegramChatID);
                }

                // Bark
                if (config.EnableBark) {
                    if (string.IsNullOrEmpty(config.BarkAddress))
                        throw new Exception(message: ConfigValidatorStrings.noBarkAddress);
                    if (string.IsNullOrEmpty(config.BarkToken))
                        throw new Exception(message: ConfigValidatorStrings.noBarkToken);
                }

                // Email
                if (config.EnableEmail) {
                    if (string.IsNullOrEmpty(config.FromEmailAddress))
                        throw new Exception(message: ConfigValidatorStrings.noFromEmailAddress);
                    if (string.IsNullOrEmpty(config.ToEmailAddress))
                        throw new Exception(message: ConfigValidatorStrings.noToEmailAddress);
                    if (string.IsNullOrEmpty(config.SMTPServer))
                        throw new Exception(message: ConfigValidatorStrings.noSMTPServer);
                    if (string.IsNullOrEmpty(config.AuthAccount))
                        throw new Exception(message: ConfigValidatorStrings.noAuthAccount);
                    if (string.IsNullOrEmpty(config.AuthPassword))
                        throw new Exception(message: ConfigValidatorStrings.noAuthPassword);
                }

				// QQ Http
				if (config.EnableQQHttp) {
					if (string.IsNullOrEmpty(config.QQHttpAddress))
						throw new Exception(message: "No QQ http address provided!");
					if (string.IsNullOrEmpty(config.QQHttpPort.ToString()))
						throw new Exception(message: "No QQ http port provided!");
					if (string.IsNullOrEmpty(config.ToQQID))
						throw new Exception(message: "No QQ ID provided!");
					if (string.IsNullOrEmpty(config.QQHttpToken))
						_logger.LogInformation("No QQ Http token provided, make sure to set it right if token is enabled in your server settings.");
				}

				// QQ WebSocket
				if (config.EnableQQWebSocket) {
					if (string.IsNullOrEmpty(config.QQWebSocketAddress))
						throw new Exception(message: "No QQ WebSocket address provided!");
					if (string.IsNullOrEmpty(config.QQWebSocketPort.ToString()))
						throw new Exception(message: "No QQ WebSocket port provided!");
					if (string.IsNullOrEmpty(config.QQWebSocketToken))
						throw new Exception(message: "No QQ WebSocket token provided!");
					if (string.IsNullOrEmpty(config.ToQQID))
						throw new Exception(message: "No QQ ID provided!");
					if (string.IsNullOrEmpty(config.QQWebSocketToken))
						_logger.LogInformation("No QQ WebSocket token provided, make sure to set it right if token is enabled in your server settings.");
				}

				// PushPlus
				if (config.EnablePushPlus) {
                    if (string.IsNullOrEmpty(config.PushPlusToken))
                        throw new Exception(message: ConfigValidatorStrings.noPushPlusToken);
                }

                // DingTalk
                if (config.EnableDingTalk) {
                    if (string.IsNullOrEmpty(config.DingTalkBotToken))
                        throw new Exception(message: ConfigValidatorStrings.noDingTalkToken);
                }

                // PushDeer
                if (config.EnablePushDeer) {
                    if (string.IsNullOrEmpty(config.PushDeerToken))
                        throw new Exception(message: ConfigValidatorStrings.noPushDeerToken);
                }

                // Discord
                if (config.EnableDiscord) {
                    if (string.IsNullOrEmpty(config.DiscordWebhookURL))
                        throw new Exception(message: ConfigValidatorStrings.noDiscordWebhook);
                }

                // Meow
                if (config.EnableMeow) {
                    if (string.IsNullOrEmpty(config.MeowAddress))
                        throw new Exception(message: ConfigValidatorStrings.noMeowAddress);
                    if (string.IsNullOrEmpty(config.MeowNickname))
                        throw new Exception(message: ConfigValidatorStrings.noMeowNickname);
                }

                _logger.LogDebug($"Done: {ConfigValidatorStrings.debugCheckValid}");
            } catch (Exception) {
                _logger.LogError($"Error: {ConfigValidatorStrings.debugCheckValid}");
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
