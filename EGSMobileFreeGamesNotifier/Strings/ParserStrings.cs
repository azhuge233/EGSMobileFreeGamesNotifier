namespace EGSMobileFreeGamesNotifier.Strings {
    internal class ParserStrings {
        internal static readonly string giveawayUrl = "https://store.epicgames.com/mobile";

		internal static readonly string sectionModuleTypeBreakers = "breakers";
        internal static readonly string sectionBreakerListItemLabelFree = "Free";

        internal static readonly string removeSpecialCharsRegex = @"[^0-9a-zA-Z]+";

        #region debug strings
        internal static readonly string debugParse = "Parse";

        internal static readonly string debugNoJsonData = "No JSON data found";
        internal static readonly string debugNoFreeSection = "No free section found";

        internal static readonly string debugFoundInfo = "Found info: {0}";
        internal static readonly string debugFoundInPreviousRecord = "Found in previous records: {0}";
        internal static readonly string debugFoundNewRecord = "Found new record: {0}";
        #endregion
    }
}
