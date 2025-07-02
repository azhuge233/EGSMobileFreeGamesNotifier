using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Scraper : IDisposable {
        private readonly ILogger<Scraper> _logger;

		public Scraper(ILogger<Scraper> logger) {
            _logger = logger;

			Microsoft.Playwright.Program.Main(["install", "firefox"]);
		}

		internal async Task<Tuple<string, string>> GetSource() {
            try {
                _logger.LogDebug(ScraperStrings.debugGetSource);

				var androidContent = await TryGetDataWithPlaywright(ScraperStrings.UrlAndroid, "android");
				var iosContent = await TryGetDataWithPlaywright(ScraperStrings.UrlIOS, "ios");

				_logger.LogDebug($"Done: {ScraperStrings.debugGetSource}");
                return new (androidContent, iosContent);
            } catch (Exception) {
                _logger.LogError($"Error: {ScraperStrings.debugGetSource}");
                throw;
            } finally {
                Dispose();
            }
        }

		private async Task<string> TryGetDataWithPlaywright(string url, string platform) {
			try {
                _logger.LogDebug($"{ScraperStrings.debugTryMessagePlaywright} | {platform}");

				var page = await GetNewPageInstance();

				await page.GotoAsync(url, new() { WaitUntil = WaitUntilState.NetworkIdle });

				var source = await page.Locator("body > pre").InnerTextAsync();

				await page.CloseAsync();

				_logger.LogDebug($"Done: {ScraperStrings.debugTryMessagePlaywright} | {platform}");
				return source;
			} catch (Exception) {
				_logger.LogError($"Error: {ScraperStrings.debugTryMessagePlaywright} | {platform}");
				throw;
			}
		}

		private static async Task<IPage> GetNewPageInstance() {
			var playwright = await Playwright.CreateAsync();
			var browser = await playwright.Firefox.LaunchAsync(new() { Headless = true });

			var page = await browser.NewPageAsync();
			page.SetDefaultTimeout(10000);
			page.SetDefaultNavigationTimeout(10000);
			await page.RouteAsync("**/*", async route => {
				var blockList = new List<string> { "stylesheet", "image", "font" };
				if (blockList.Contains(route.Request.ResourceType)) await route.AbortAsync();
				else await route.ContinueAsync();
			});

			return page;
		}

		public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
