namespace EGSMobileFreeGamesNotifier.Strings {
    internal class ScraperStrings {
		private static readonly string UrlBase = @"https://egs-platform-service.store.epicgames.com/api/v2/public/discover/home?count=1&start=2&country=US&locale=en-US&store=EGS&platform=";

		internal static readonly string UrlAndroid = $"{UrlBase}android";
        internal static readonly string UrlIOS = $"{UrlBase}ios";

		#region headers string
		internal static readonly string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36 Edg/134.0.0.0";
        internal static readonly string CacheControlKey = "Cache-Control";
		internal static readonly string CacheControlValue = "no-cache";
        internal static readonly string PragmaKey = "Pragma";
		internal static readonly string PragmaValue = "no-cache";
		#endregion

		#region debug strings
		internal static readonly string debugGetSource = "Get source";
        internal static readonly string debugGetSourceWithUrl = "Getting source: {0}";
        #endregion
    }
}
