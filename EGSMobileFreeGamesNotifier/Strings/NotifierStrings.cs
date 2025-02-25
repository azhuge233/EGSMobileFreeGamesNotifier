using System;
using System.Collections.Generic;
using System.Linq;

namespace EGSMobileFreeGamesNotifier.Strings
{
    internal class NotifierStrings
    {
        #region SendMessage()
        internal static readonly string debugSendMessageBark = "Send notification to Bark";

        internal static readonly string debugSendMessageDingTalk = "Send notifications to DingTalk";

        internal static readonly string debugSendMessageDiscord = "Send notification to Discord";

        internal static readonly string debugSendMessageEmail = "Send notification to Email";

        internal static readonly string debugSendMessagePushDeer = "Send notification to PushDeer";

        internal static readonly string debugSendMessagePushPlus = "Send notification to PushPlus";

        internal static readonly string debugSendMessageQQ = "Send notifications to QQ";

        internal static readonly string debugSendMessageQQRed = "Send notifications to QQ Red (Chronocat)";
        internal static readonly string debugWSReconnectionQQRed = "Reconnection happened, type: {0}";
        internal static readonly string debugWSMessageRecievedQQRed = "Message received: {0}";
        internal static readonly string debugWSDisconnectedQQRed = "Disconnected: {0}";

        internal static readonly string debugSendMessageTelegram = "Send notification to Telegram";

        internal static readonly string debugSendMessageMeow = "Send notification to Meow";
        #endregion


        internal static readonly string debugCreateMessage = "Create notification message";
    }
}
