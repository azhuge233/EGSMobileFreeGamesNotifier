using System.Text.RegularExpressions;
using EGSMobileFreeGamesNotifier.Strings;

namespace EGSMobileFreeGamesNotifier.Models.Record {
    public class NotifyRecord : FreeGameRecord {
        private readonly int MessageIndex = 0;

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

		public NotifyRecord(FreeGameRecord parentRecord, int messageIndex) {
			SandboxID = parentRecord.SandboxID;
			OfferIDAndroid = parentRecord.OfferIDAndroid;
			OfferIDIOS = parentRecord.OfferIDIOS;
			UrlAndroid = parentRecord.UrlAndroid;
			UrlIOS = parentRecord.UrlIOS;
			PurchaseUrlAndroid = parentRecord.PurchaseUrlAndroid;
			PurchaseUrlIOS = parentRecord.PurchaseUrlIOS;
			Title = parentRecord.Title;
            MessageIndex = messageIndex;
		}

		public string ToTelegramMessage() {
            return string.Format(NotifyFormatStrings.telegramPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS, RemoveSpecialCharacters(Title));
        }

        public string ToBarkMessage() {
            return string.Format(NotifyFormatStrings.barkPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToEmailMessage() {
            return string.Format(NotifyFormatStrings.emailPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToQQMessage() {
            return string.Format(NotifyFormatStrings.qqPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToPushPlusMessage() {
            return string.Format(NotifyFormatStrings.pushPlusPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToDingTalkMessage() {
            return string.Format(NotifyFormatStrings.dingTalkPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToPushDeerMessage() {
            return string.Format(NotifyFormatStrings.pushDeerPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToDiscordMessage() {
            return string.Format(NotifyFormatStrings.discordPushFormat[MessageIndex], UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

        public string ToMeowMessage() {
            return string.Format(NotifyFormatStrings.meowPushFormat[MessageIndex], Title, UrlAndroid, UrlIOS, PurchaseUrlAndroid, PurchaseUrlIOS);
        }

		private static string RemoveSpecialCharacters(string str) {
			return Regex.Replace(str, ParserStrings.removeSpecialCharsRegex, string.Empty);
		}
	}
}
