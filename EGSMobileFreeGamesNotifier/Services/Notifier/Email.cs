using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class Email(ILogger<Email> logger, IOptions<Config> config) : INotifiable {
        private readonly ILogger<Email> _logger = logger;
        private readonly Config config = config.Value;

        public async Task SendMessage(List<NotifyRecord> records) {
            try {
                _logger.LogDebug(NotifierStrings.debugSendMessageEmail);

                var message = CreateMessage(records, config.FromEmailAddress, config.ToEmailAddress);

                using var client = new SmtpClient();

                client.Connect(config.SMTPServer, config.SMTPPort, true);
                client.Authenticate(config.AuthAccount, config.AuthPassword);

                await client.SendAsync(message);

                client.Disconnect(true);

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageEmail}");
            } catch (Exception) {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessageEmail}");
                throw;
            } finally {
                Dispose();
            }
        }

        private MimeMessage CreateMessage(List<NotifyRecord> pushList, string fromAddress, string toAddress) {
            try {
                _logger.LogDebug(NotifierStrings.debugCreateMessage);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("EGSMobileFreeGamesNotifier", fromAddress));
                message.To.Add(new MailboxAddress("Receiver", toAddress));

                var sb = new StringBuilder();

                message.Subject = sb.AppendFormat(NotifyFormatStrings.emailTitleFormat, pushList.Count).ToString();
                sb.Clear();

                pushList.ForEach(record => sb.AppendFormat(NotifyFormatStrings.emailBodyFormat, record.ToEmailMessage()));

                message.Body = new TextPart("html") {
                    Text = sb.Append(NotifyFormatStrings.projectLinkHTML).ToString()
                };

                _logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
                return message;
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
