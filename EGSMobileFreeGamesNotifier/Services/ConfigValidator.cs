using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services
{
    internal class ConfigValidator(ILogger<ConfigValidator> logger) : IDisposable
    {
        private readonly ILogger<ConfigValidator> _logger = logger;

        internal void CheckValid(Config config)
        {
            try
            {
                _logger.LogDebug(ConfigValidatorStrings.debugCheckValid);

                // Telegram
                if (config.EnableTelegram)
                {
                    if (string.IsNullOrEmpty(config.TelegramToken))
                        throw new Exception(message: ConfigValidatorStrings.noTelegramToken);
                    if (string.IsNullOrEmpty(config.TelegramChatID))
                        throw new Exception(message: ConfigValidatorStrings.noTelegramChatID);
                }

                // Bark
                if (config.EnableBark)
                {
                    if (string.IsNullOrEmpty(config.BarkAddress))
                        throw new Exception(message: ConfigValidatorStrings.noBarkAddress);
                    if (string.IsNullOrEmpty(config.BarkToken))
                        throw new Exception(message: ConfigValidatorStrings.noBarkToken);
                }

                // Email
                if (config.EnableEmail)
                {
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

                // QQ
                if (config.EnableQQ)
                {
                    if (string.IsNullOrEmpty(config.QQAddress))
                        throw new Exception(message: ConfigValidatorStrings.noQQAddress);
                    if (string.IsNullOrEmpty(config.QQPort.ToString()))
                        throw new Exception(message: ConfigValidatorStrings.noQQPort);
                    if (string.IsNullOrEmpty(config.ToQQID))
                        throw new Exception(message: ConfigValidatorStrings.noToQQID);
                }

                // QQ Red (Chronocat)
                if (config.EnableRed)
                {
                    if (string.IsNullOrEmpty(config.RedAddress))
                        throw new Exception(message: ConfigValidatorStrings.noRedAddress);
                    if (string.IsNullOrEmpty(config.RedPort.ToString()))
                        throw new Exception(message: ConfigValidatorStrings.noRedPort);
                    if (string.IsNullOrEmpty(config.RedToken))
                        throw new Exception(message: ConfigValidatorStrings.noRedToken);
                    if (string.IsNullOrEmpty(config.ToQQID))
                        throw new Exception(message: ConfigValidatorStrings.noToQQID);
                }

                // PushPlus
                if (config.EnablePushPlus)
                {
                    if (string.IsNullOrEmpty(config.PushPlusToken))
                        throw new Exception(message: ConfigValidatorStrings.noPushPlusToken);
                }

                // DingTalk
                if (config.EnableDingTalk)
                {
                    if (string.IsNullOrEmpty(config.DingTalkBotToken))
                        throw new Exception(message: ConfigValidatorStrings.noDingTalkToken);
                }

                // PushDeer
                if (config.EnablePushDeer)
                {
                    if (string.IsNullOrEmpty(config.PushDeerToken))
                        throw new Exception(message: ConfigValidatorStrings.noPushDeerToken);
                }

                // Discord
                if (config.EnableDiscord)
                {
                    if (string.IsNullOrEmpty(config.DiscordWebhookURL))
                        throw new Exception(message: ConfigValidatorStrings.noDiscordWebhook);
                }

                // Meow
                if (config.EnableMeow)
                {
                    if (string.IsNullOrEmpty(config.MeowAddress))
                        throw new Exception(message: ConfigValidatorStrings.noMeowAddress);
                    if (string.IsNullOrEmpty(config.MeowNickname))
                        throw new Exception(message: ConfigValidatorStrings.noMeowNickname);
                }

                _logger.LogDebug($"Done: {ConfigValidatorStrings.debugCheckValid}");
            }
            catch (Exception)
            {
                _logger.LogError($"Error: {ConfigValidatorStrings.debugCheckValid}");
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
