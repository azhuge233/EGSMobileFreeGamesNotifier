namespace EGSMobileFreeGamesNotifier.Strings {
    internal class NotifyFormatStrings {
        #region ToMessage() strings
        internal static readonly string telegramPushFormat = "<b>EGS Mobile Free Games</b>\n\n" +
            "<i>{0}</i>\n\n" +
            "{1}\n\n" +
            "{2}\n\n" +
            "#EGSMobileFreeGamesNotifier";

        internal static readonly string barkPushFormat = "{0}\n\n" +
            "{1}\n\n" +
            "{2}\n\n";

        internal static readonly string emailPushFormat = "<b>{0}</b><br>" +
            "{1}<br>" +
            "<a href=\"{2}\">{2}</a><br>";

        internal static readonly string qqPushFormat = "EGSMobileFreeGamesNotifier\n\n" +
            "{0}\n\n" +
            "{1}\n" +
            "{2}";

        internal static readonly string pushPlusPushFormat = "<b>{0}</b><br>" +
            "{1}<br>" +
            "<a href=\"{2}\">{2}</a><br>";

        internal static readonly string dingTalkPushFormat = "EGSMobileFreeGamesNotifier\n\n" +
            "{0}\n\n" +
            "{1}\n" +
            "{2}\n";

        internal static readonly string pushDeerPushFormat = "EGSMobileFreeGamesNotifier\n\n" +
            "{0}\n" +
            "{1}\n" +
            "{2}\n";

        internal static readonly string discordPushFormat = "{0}\n";

        internal static readonly string meowPushFormat = "{0}\n\n" +
            "{1}\n";
        #endregion

        #region url, title format strings
        internal static readonly string barkUrlFormat = "{0}/{1}/";
        internal static readonly string barkUrlTitle = "EGSMobileFreeGamesNotifier/";
        internal static readonly string barkUrlArgs = "?group=EGSMobileFreeGamesNotifier" +
            "&isArchive=1" +
            "&sound=calypso" +
            // "&url={0}" +
            "&copy={0}";

        internal static readonly string emailTitleFormat = "{0} new free game(s) - EGSMobileFreeGamesNotifier";
        internal static readonly string emailBodyFormat = "<br>{0}";

        internal static readonly string qqHttpUrlFormat = "http://{0}:{1}/send_private_msg?access_token={2}";
        internal static readonly string qqWebSocketUrlFormat = "ws://{0}:{1}/?access_token={2}";
        internal static readonly string qqWebSocketSendAction = "send_private_msg";

        internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - EGSMobileFreeGamesNotifier";
        internal static readonly string pushPlusBodyFormat = "<br>{0}";
        internal static readonly string pushPlusUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";
        internal static readonly string pushPlusPostUrl = "http://www.pushplus.plus/send";

        internal static readonly string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

        internal static readonly string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";

        internal static readonly string discordTitleNewFreeGame = "New Free Game - EGSMobileFreeGamesNotifier";
        internal static readonly string discordTitleNewFreeGames = "New Free Games - EGSMobileFreeGamesNotifier";

        internal static readonly string meowUrlFormat = "{0}/{1}";
        internal static readonly string meowUrlTitle = "EGSMobileFreeGamesNotifier";
        #endregion

        internal static readonly string projectLink = "\n\nFrom https://github.com/azhuge233/EGSMobileFreeGamesNotifier";
        internal static readonly string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/EGSMobileFreeGamesNotifier\">EGSMobileFreeGamesNotifier</a>";
    }
}
