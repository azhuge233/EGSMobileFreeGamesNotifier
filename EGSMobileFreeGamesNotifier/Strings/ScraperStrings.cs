namespace EGSMobileFreeGamesNotifier.Strings {
    internal class ScraperStrings {
		private static readonly string UrlBase = @"https://egs-platform-service.store.epicgames.com/api/v2/public/discover/home?count=10&start=0&country=US&locale=en-US&store=EGS&platform=";

		internal static readonly string UrlAndroid = $"{UrlBase}android";
        internal static readonly string UrlIOS = $"{UrlBase}ios";

		#region headers string
		internal static readonly string UserAgentKey = "User-Agent";
		internal static readonly string UserAgentValue = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36 Edg/137.0.0.0";
        internal static readonly string CacheControlKey = "Cache-Control";
		internal static readonly string CacheControlValue = "no-cache";
        internal static readonly string PragmaKey = "Pragma";
		internal static readonly string PragmaValue = "no-cache";
		internal static readonly string CookieKey = "Cookie";
		internal static readonly string CookieValue = "__cf_bm=; __cflb=; _cfuvid=; cf_clearance=;";
		internal static readonly string AcceptKey = "Accept";
		internal static readonly string AcceptValue = "application/json";
		internal static readonly string AcceptLanguageKey = "Accept-Language";
		internal static readonly string AcceptLanguageValue = "en-US,en;q=0.9";
		#endregion

		#region debug strings
		internal static readonly string debugGetSource = "Get source";
        internal static readonly string debugGetSourceWithUrl = "Getting source: {0}";
		internal static readonly string debugTryMessage = "Trying to get json data: {0} | {1}";

		internal static readonly string errorGetDataFailed = "Failed to get data from {0} after {1} tries";
		#endregion
	}
}
