using EGSMobileFreeGamesNotifier.Modules;
using EGSMobileFreeGamesNotifier.Services;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace EGSMobileFreeGamesNotifier
{
    internal class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static async Task Main()
        {
            try
            {
                var servicesProvider = DI.BuildDiAll();

                logger.Info(" - Start Job -");

                using (servicesProvider as IDisposable)
                {
                    var jsonOp = servicesProvider.GetRequiredService<Json>();
                    var notifyOP = servicesProvider.GetRequiredService<Notify>();

                    var config = jsonOp.LoadConfig();
                    var oldRecord = jsonOp.LoadData();

                    servicesProvider.GetRequiredService<ConfigValidator>().CheckValid(config);

                    // Get page source
                    var source = await servicesProvider.GetRequiredService<Scraper>().GetSource();

                    // Parse page source
                    var parseResult = servicesProvider.GetRequiredService<Parser>().Parse(source, oldRecord);

                    // Notify first, then write records
                    await notifyOP.DoNotify(config, parseResult.NotifyRecords);

                    // Write new records
                    jsonOp.WriteData(parseResult.Records);
                }

                logger.Info(" - Job End -\n");
            }
            catch (Exception ex)
            {
                logger.Error($"{ex.Message}\n");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
