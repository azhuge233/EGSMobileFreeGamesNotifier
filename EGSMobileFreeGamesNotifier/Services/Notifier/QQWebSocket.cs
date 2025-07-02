using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Models.WebSocketContent;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;
using System.Text.Json;
using Websocket.Client;

namespace EGSMobileFreeGamesNotifier.Services.Notifier {
    internal class QQWebSocket(ILogger<QQWebSocket> logger, IOptions<Config> config) : INotifiable {
        private readonly ILogger<QQWebSocket> _logger = logger;
        private readonly Config config = config.Value;

		public async Task SendMessage(List<NotifyRecord> records) {
            try {
                _logger.LogDebug(NotifierStrings.debugSendMessageQQWebSocket);

                var packets = GetSendPacket(config, records);

                using var client = GetWSClient(config);

                await client.Start();

                foreach (var packet in packets) {
                    await client.SendInstant(JsonSerializer.Serialize(packet));
                    await Task.Delay(600);
                }

                await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQWebSocket}");
            } catch (Exception) {
                _logger.LogDebug($"Error: {NotifierStrings.debugSendMessageQQWebSocket}");
                throw;
            } finally {
                Dispose();
            }
        }

        private WebsocketClient GetWSClient(NotifyConfig config) {
            var url = new Uri(string.Format(NotifyFormatStrings.qqWebSocketUrlFormat, config.QQWebSocketAddress, config.QQWebSocketPort, config.QQWebSocketToken));

            #region new websocket client
            var client = new WebsocketClient(url);
            client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(NotifierStrings.debugWSReconnectionQQWebSocket, info.Type));
            client.MessageReceived.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSMessageRecievedQQWebSocket, msg));
            client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSDisconnectedQQWebSocket, msg));
            #endregion

            return client;
        }

        private static List<WSPacket> GetSendPacket(NotifyConfig config, List<NotifyRecord> records) {
            return records.Select(record => new WSPacket() {
                Action = NotifyFormatStrings.qqWebSocketSendAction,
                Params = new Param { 
                    UserID = config.ToQQID,
                    Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}"
                }
            }).ToList();
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
