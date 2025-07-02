using EGSMobileFreeGamesNotifier.Models.Config;
using EGSMobileFreeGamesNotifier.Services;
using EGSMobileFreeGamesNotifier.Services.Notifier;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Modules {
    internal class DI {
		private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

		private static readonly IConfigurationRoot logConfig = new ConfigurationBuilder().SetBasePath(BasePath).Build();
        private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(BasePath)
            .AddJsonFile($"Config{Path.DirectorySeparatorChar}config.json", optional: false, reloadOnChange: true)
            .Build();

        internal static IServiceProvider BuildDiAll() {
            return new ServiceCollection()
               .AddTransient<Json>()
               .AddTransient<ConfigValidator>()
               .AddTransient<Scraper>()
               .AddTransient<Parser>()
               .AddTransient<Notify>()
               .AddTransient<Bark>()
               .AddTransient<TelegramBot>()
               .AddTransient<Email>()
               .AddTransient<QQHttp>()
               .AddTransient<QQWebSocket>()
               .AddTransient<PushPlus>()
               .AddTransient<DingTalk>()
               .AddTransient<PushDeer>()
               .AddTransient<Discord>()
               .AddTransient<Meow>()
               .Configure<Config>(configuration)
			   .AddLogging(loggingBuilder => {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(logConfig);
               })
               .BuildServiceProvider();
        }
    }
}
