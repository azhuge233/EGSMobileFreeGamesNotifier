using System.Text.RegularExpressions;
using EGSMobileFreeGamesNotifier.Strings;

namespace EGSMobileFreeGamesNotifier.Models.Record {
    public class NotifyRecord : FreeGameRecord {
        public NotifyRecord(FreeGameRecord parentRecord) {
			SandboxID = parentRecord.SandboxID;
			OfferIDAndroid = parentRecord.OfferIDAndroid;
			OfferIDIOS = parentRecord.OfferIDIOS;
			UrlAndroid = parentRecord.UrlAndroid;
			UrlIOS = parentRecord.UrlIOS;
			PurchaseUrlAndroid = parentRecord.PurchaseUrlAndroid;
			PurchaseUrlIOS = parentRecord.PurchaseUrlIOS;
			Title = parentRecord.Title;
        }

        public string ToTelegramMessage() {
            return string.Format(NotifyFormatStrings.telegramPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS, RemoveSpecialCharacters(Title));
        }

        public string ToBarkMessage() {
            return string.Format(NotifyFormatStrings.barkPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToEmailMessage() {
            return string.Format(NotifyFormatStrings.emailPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToQQMessage() {
            return string.Format(NotifyFormatStrings.qqPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToPushPlusMessage() {
            return string.Format(NotifyFormatStrings.pushPlusPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToDingTalkMessage() {
            return string.Format(NotifyFormatStrings.dingTalkPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToPushDeerMessage() {
            return string.Format(NotifyFormatStrings.pushDeerPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToDiscordMessage() {
            return string.Format(NotifyFormatStrings.discordPushFormat, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToMeowMessage() {
            return string.Format(NotifyFormatStrings.meowPushFormat, Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

		private static string RemoveSpecialCharacters(string str) {
			return Regex.Replace(str, ParserStrings.removeSpecialCharsRegex, string.Empty);
		}
	}
}
