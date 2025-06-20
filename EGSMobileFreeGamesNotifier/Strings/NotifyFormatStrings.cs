namespace EGSMobileFreeGamesNotifier.Strings {
    internal class NotifyFormatStrings {
        #region ToMessage() strings
        internal static readonly string[] telegramPushFormat = [
			"<b>EGS Mobile Free Games</b>\n\n" +
            "<i>{0}</i>\n\n" +
			"Android Store Link: <a href=\"{1}\">{0}</a>\n" +
			"iOS Store Link: <a href=\"{2}\">{0}</a>\n\n" +
			"Android Claim Link: <a href=\"{3}\">Android Purchase</a>\n" +
			"iOS Claim Link: <a href=\"{4}\">iOS Purchase</a>\n\n" +
            "#{5} #EGSMobileFreeGamesNotifier",

			"<b>EGS Mobile Free Games</b> <i>Update</i>\n\n" +
			"<i>{0}</i>\n\n" +
			"Android Store Link: <a href=\"{1}\">{0}</a>\n" +
			"iOS Store Link: <a href=\"{2}\">{0}</a>\n\n" +
			"Android Claim Link: <a href=\"{3}\">Android Purchase</a>\n" +
			"iOS Claim Link: <a href=\"{4}\">iOS Purchase</a>\n\n" +
			"#{5} #EGSMobileFreeGamesNotifier #Update"
		];

        internal static readonly string[] barkPushFormat = [
			"EGS Mobile Free Games\n\n" +
			"{0}\n\n" +
            "Android Store Link: {1}\n" +
            "iOS Store Link: {2}\n\n" +
            "Android Claim Link: {3}\n" + 
            "iOS Claim Link: {4}",

			"EGS Mobile Free Games Update\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}"
		];

        internal static readonly string[] emailPushFormat = [
			"<b>{0}</b><br>" +
			"Android Store Link: <a href=\"{1}\">{0}</a><br>" +
			"iOS Store Link: <a href=\"{2}\">{0}</a><br><br>" +
			"Android Claim Link: <a href=\"{3}\">Android Purchase</a><br>" +
			"iOS Claim Link: <a href=\"{4}\">iOS Purchase</a><br>",

			"<b>{0}</b><br> <i>Update</i><br>" +
			"Android Store Link: <a href=\"{1}\">{0}</a><br>" +
			"iOS Store Link: <a href=\"{2}\">{0}</a><br><br>" +
			"Android Claim Link: <a href=\"{3}\">Android Purchase</a><br>" +
			"iOS Claim Link: <a href=\"{4}\">iOS Purchase</a><br>"
		];

        internal static readonly string[] qqPushFormat = [
			"EGS Mobile Free Games\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}",

			"EGS Mobile Free Games Update\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}"
		];

		internal static readonly string[] pushPlusPushFormat = [
			"<b>{0}</b><br>" +
			"Android Store Link: <a href=\"{1}\">{0}</a><br>" +
			"iOS Store Link: <a href=\"{2}\">{0}</a><br><br>" +
			"Android Claim Link: <a href=\"{3}\">Android Purchase</a><br>" +
			"iOS Claim Link: <a href=\"{4}\">iOS Purchase</a><br>",

			"<b>{0}</b><br> <i>Update</i><br>" +
			"Android Store Link: <a href=\"{1}\">{0}</a><br>" +
			"iOS Store Link: <a href=\"{2}\">{0}</a><br><br>" +
			"Android Claim Link: <a href=\"{3}\">Android Purchase</a><br>" +
			"iOS Claim Link: <a href=\"{4}\">iOS Purchase</a><br>"
		];

		internal static readonly string[] dingTalkPushFormat = [
			"EGS Mobile Free Games\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}",

			"EGS Mobile Free Games Update\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}"
		];

		internal static readonly string[] pushDeerPushFormat = [
			"EGS Mobile Free Games\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}",

			"EGS Mobile Free Games Update\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}"
		];

		internal static readonly string[] discordPushFormat = [
			"Android Store Link: {0}\n" +
			"iOS Store Link: {1}\n\n" +
			"Android Claim Link: {2}\n" +
			"iOS Claim Link: {3}",

			"Update\n\n" +
			"Android Store Link: {0}\n" +
			"iOS Store Link: {1}\n\n" +
			"Android Claim Link: {2}\n" +
			"iOS Claim Link: {3}"
		];

		internal static readonly string[] meowPushFormat = [
			"EGS Mobile Free Games\n\n" +
			"{0}\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}",

			"EGS Mobile Free Games Update\n\n" +
			"{0} Update\n\n" +
			"Android Store Link: {1}\n" +
			"iOS Store Link: {2}\n\n" +
			"Android Claim Link: {3}\n" +
			"iOS Claim Link: {4}"
		];
		#endregion

		#region url, title format strings
		internal static readonly string barkUrlFormat = "{0}/{1}/";
        internal static readonly string barkUrlTitle = "EGSMobileFreeGamesNotifier/";
        internal static readonly string barkUrlArgs = "?group=EGSMobileFreeGamesNotifier" +
            "&isArchive=1" +
            "&sound=calypso" +
            // "&url={0}" +
            "&copy={0}";

        internal static readonly string emailTitleFormat = "{0} new free game(s) - EGS Mobile Free Games";
        internal static readonly string emailBodyFormat = "<br>{0}";

        internal static readonly string qqHttpUrlFormat = "http://{0}:{1}/send_private_msg?access_token={2}";
        internal static readonly string qqWebSocketUrlFormat = "ws://{0}:{1}/?access_token={2}";
        internal static readonly string qqWebSocketSendAction = "send_private_msg";

        internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - EGS Mobile Free Games";
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
