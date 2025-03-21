using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Scraper: IDisposable {
        private readonly ILogger<Scraper> _logger;

        internal HttpClient Client { get; set; } = new();

        public Scraper(ILogger<Scraper> logger) {
            _logger = logger;
            Client.DefaultRequestHeaders.UserAgent.ParseAdd(ScraperStrings.UserAgent);
            Client.DefaultRequestHeaders.Add(ScraperStrings.CacheControlKey, ScraperStrings.CacheControlValue);
			Client.DefaultRequestHeaders.Add(ScraperStrings.PragmaKey, ScraperStrings.PragmaValue);
		}

        internal async Task<Tuple<string, string>> GetSource() {
            try {
                _logger.LogDebug(ScraperStrings.debugGetSource);

                // send one dummy request first to get cloudflare validation passed
                var resp = await Client.GetAsync(ScraperStrings.UrlAndroid);

				resp = await Client.GetAsync(ScraperStrings.UrlAndroid);
				var androidContent = await resp.Content.ReadAsStringAsync();

				resp = await Client.GetAsync(ScraperStrings.UrlIOS);
                var iosContent = await resp.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScraperStrings.debugGetSource}");
                return new (androidContent, iosContent);
            } catch (Exception) {
                _logger.LogError($"Error: {ScraperStrings.debugGetSource}");
                throw;
            } finally {
                Dispose();
            }
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
