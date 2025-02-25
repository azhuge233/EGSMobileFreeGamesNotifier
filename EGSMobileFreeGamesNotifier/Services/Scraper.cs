using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Scraper(ILogger<Scraper> logger): IDisposable {
        private readonly ILogger<Scraper> _logger = logger;

        internal HttpClient Client { get; set; } = new();

        internal async Task<string> GetSource() {
            try {
                _logger.LogDebug(ScraperStrings.debugGetSource);

                var resp = await Client.GetAsync(ScraperStrings.Url);
                var content = await resp.Content.ReadAsStringAsync();

                _logger.LogDebug($"Done: {ScraperStrings.debugGetSource}");
                return content;
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
