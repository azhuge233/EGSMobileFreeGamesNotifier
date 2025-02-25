using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Models.WebSocketContent;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Websocket.Client;

namespace EGSMobileFreeGamesNotifier.Services.Notifier
{
    internal class QQRed(ILogger<QQRed> logger) : INotifiable
    {
        private readonly ILogger<QQRed> _logger = logger;

        public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records)
        {
            try
            {
                _logger.LogDebug(NotifierStrings.debugSendMessageQQRed);

                var packets = GetSendPacket(config, records);

                using var client = GetWSClient(config);

                await client.Start();

                await client.SendInstant(JsonSerializer.Serialize(GetConnectPacket(config)));

                foreach (var packet in packets)
                {
                    await client.SendInstant(JsonSerializer.Serialize(packet));
                    await Task.Delay(600);
                }

                await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

                _logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQRed}");
            }
            catch (Exception)
            {
                _logger.LogDebug($"Error: {NotifierStrings.debugSendMessageQQRed}");
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        private WebsocketClient GetWSClient(NotifyConfig config)
        {
            var url = new Uri(new StringBuilder().AppendFormat(NotifyFormatStrings.qqRedUrlFormat, config.RedAddress, config.RedPort).ToString());

            #region new websocket client
            var client = new WebsocketClient(url);
            client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(NotifierStrings.debugWSReconnectionQQRed, info.Type));
            client.MessageReceived.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSMessageRecievedQQRed, msg));
            client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSDisconnectedQQRed, msg));
            #endregion

            return client;
        }

        private static WSPacket GetConnectPacket(NotifyConfig config)
        {
            return new WSPacket()
            {
                Type = NotifyFormatStrings.qqRedWSConnectPacketType,
                Payload = new ConnectPayload()
                {
                    Token = config.RedToken
                }
            };
        }

        private static List<WSPacket> GetSendPacket(NotifyConfig config, List<NotifyRecord> records)
        {
            return records.Select(record => new WSPacket()
            {
                Type = NotifyFormatStrings.qqRedWSSendPacketType,
                Payload = new MessagePayload()
                {
                    Peer = new Peer()
                    {
                        ChatType = 1,
                        PeerUin = config.ToQQID
                    },
                    Elements = [
                        new TextElementRoot() {
                            TextElement = new TextElement() {
                                Content = new StringBuilder().Append(record.ToQQMessage())
                                                            .Append(NotifyFormatStrings.projectLink)
                                                            .ToString()
                            }
                        }
                    ]
                }
            }).ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
