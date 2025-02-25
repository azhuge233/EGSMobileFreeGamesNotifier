using System.Text.RegularExpressions;
using EGSMobileFreeGamesNotifier.Strings;

namespace EGSMobileFreeGamesNotifier.Models.Record {
    public class NotifyRecord : FreeGameRecord {
        public NotifyRecord(FreeGameRecord parentRecord) {
            Title = parentRecord.Title;
            Url = parentRecord.Url;
            Description = parentRecord.Description;
            Disclaimer = parentRecord.Disclaimer;
        }

        public string ToTelegramMessage() {
            return string.Format(NotifyFormatStrings.telegramPushFormat, Description, Disclaimer, Url);
        }

        public string ToBarkMessage() {
            return string.Format(NotifyFormatStrings.barkPushFormat, Description, Disclaimer, Url);
        }

        public string ToEmailMessage() {
            return string.Format(NotifyFormatStrings.emailPushFormat, Description, Disclaimer, Url);
        }

        public string ToQQMessage() {
            return string.Format(NotifyFormatStrings.qqPushFormat, Description, Disclaimer, Url);
        }

        public string ToPushPlusMessage() {
            return string.Format(NotifyFormatStrings.pushPlusPushFormat, Description, Disclaimer, Url);
        }

        public string ToDingTalkMessage() {
            return string.Format(NotifyFormatStrings.dingTalkPushFormat, Description, Disclaimer, Url);
        }

        public string ToPushDeerMessage() {
            return string.Format(NotifyFormatStrings.pushDeerPushFormat, Description, Disclaimer, Url);
        }

        public string ToDiscordMessage() {
            return string.Format(NotifyFormatStrings.discordPushFormat, Disclaimer);
        }

        public string ToMeowMessage() {
            return string.Format(NotifyFormatStrings.meowPushFormat, Description, Disclaimer, Url);
        }
    }
}
