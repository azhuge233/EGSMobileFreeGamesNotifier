namespace EGSMobileFreeGamesNotifier.Strings {
    internal class ParserStrings {
        internal static readonly string storeBaseUrl = @"https://store.epicgames.com/p/";
        internal static readonly string purchaseBaseUrl = @"https://store.epicgames.com/purchase?offers=1-{0}-{1}";


		internal static readonly string freeDataValue = "freeGame";

		internal static readonly string removeSpecialCharsRegex = @"[^0-9a-zA-Z]+";

        #region debug strings
        internal static readonly string debugParse = "Parse";
        internal static readonly string debugParseSource = "Parse Source";

        internal static readonly string debugNoJsonData = "No JSON data found";
        internal static readonly string debugNoFreeSection = "No free section found";
        internal static readonly string debugNoFreeOffers = "No free offers found";

        internal static readonly string debugFoundInfo = "Found game: {0}";
        internal static readonly string debugInfoInAndroid = "Found same game in Android: {0}";

        internal static readonly string debugFoundInPreviousRecord = "Found in previous records: {0}";
        internal static readonly string debugFoundNewRecord = "Found new record: {0}";
        #endregion
    }
}
