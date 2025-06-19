using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Scraper: IDisposable {
        private readonly ILogger<Scraper> _logger;

        private readonly int MaxTries = 3;

		private readonly HttpClient Client;
		private readonly CookieContainer _cookieContainer = new();

		public Scraper(ILogger<Scraper> logger) {
            _logger = logger;

			var handler = new HttpClientHandler {
				CookieContainer = _cookieContainer,
				UseCookies = true
			};

			Client = new(handler);

            Client.DefaultRequestHeaders.Add(ScraperStrings.UserAgentKey, ScraperStrings.UserAgentValue);
            Client.DefaultRequestHeaders.Add(ScraperStrings.CacheControlKey, ScraperStrings.CacheControlValue);
            Client.DefaultRequestHeaders.Add(ScraperStrings.PragmaKey, ScraperStrings.PragmaValue);
            Client.DefaultRequestHeaders.Add(ScraperStrings.AcceptKey, ScraperStrings.AcceptValue);
            Client.DefaultRequestHeaders.Add(ScraperStrings.AcceptLanguageKey, ScraperStrings.AcceptLanguageValue);
			// Client.DefaultRequestHeaders.Add(ScraperStrings.CookieKey, ScraperStrings.CookieValue);
		}

		internal async Task<Tuple<string, string>> GetSource() {
            try {
                _logger.LogDebug(ScraperStrings.debugGetSource);

                var androidContent = await TryGetData(ScraperStrings.UrlAndroid, "android");
				var iosContent = await TryGetData(ScraperStrings.UrlIOS, "ios");

				File.WriteAllText("Test\\android.json", androidContent);
				File.WriteAllText("Test\\ios.json", iosContent);

				_logger.LogDebug($"Done: {ScraperStrings.debugGetSource}");
                return new (androidContent, iosContent);
            } catch (Exception) {
                _logger.LogError($"Error: {ScraperStrings.debugGetSource}");
                throw;
            } finally {
                Dispose();
            }
        }

		private async Task<string> TryGetData(string url, string platform) {
            try {
                int tryCount = 0;
                while (++tryCount < MaxTries) {
                    _logger.LogDebug(ScraperStrings.debugTryMessage, tryCount, platform);

                    var uri = new Uri(url);

					var resp = await Client.GetAsync(uri);
                    var tmpContent = await resp.Content.ReadAsStringAsync();

                    if (tmpContent.StartsWith('{')) return tmpContent;

                    PrintCookies(uri);

                    await Task.Delay(new Random().Next(2, 5) * 500);
				}
                throw new Exception(string.Format(ScraperStrings.errorGetDataFailed, platform, tryCount));
            } catch (Exception) {
                throw;
            }
		}

		private void PrintCookies(Uri uri) => _cookieContainer.GetCookies(uri).ToList().ForEach(cookie => _logger.LogInformation($"Cookie: {cookie.Name} = {cookie.Value}"));

		public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
