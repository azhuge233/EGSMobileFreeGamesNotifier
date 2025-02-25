using System.Text;
using System.Text.Json;
using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.PostContent;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services.Notifier
{
    internal class DingTalk(ILogger<DingTalk> logger) : INotifiable
    {
        private readonly ILogger<DingTalk> _logger = logger;

        public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records)
        {
            try
            {
                _logger.LogDebug(NotifierStrings.debugSendMessageDingTalk);

                var url = new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkUrlFormat, config.DingTalkBotToken).ToString();

                var content = new DingTalkPostContent();

                var client = new HttpClient();

                foreach (var record in records)
                {
                    content.Text.Content_ = $"{record.ToDingTalkMessage()}{NotifyFormatStrings.projectLink}";

                    var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    var resp = await client.PostAsync(url, data);

                    _logger.LogDebug(await resp.Content.ReadAsStringAsync());
                }

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageDingTalk}");
            }
            catch (Exception)
            {
                _logger.LogError($"Error: {NotifierStrings.debugSendMessageDingTalk}");
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
