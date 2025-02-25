using EGSMobileFreeGamesNotifier.Models;
using EGSMobileFreeGamesNotifier.Models.EGSJsonData;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Parser(ILogger<Parser> logger) : IDisposable {
        private readonly ILogger<Parser> _logger = logger;
        // private readonly IServiceProvider services = DI.BuildDiScraperOnly();

        public ParseResult Parse(string source, List<FreeGameRecord> records) {
            try {
                _logger.LogDebug(ParserStrings.debugParse);

                var jsonData = JsonSerializer.Deserialize<LayoutWrapper>(source);

				var parseResult = new ParseResult();

                if (jsonData != null) {
                    var sections = jsonData.Layout.Sections.Where(section => section.ModuleType == ParserStrings.sectionModuleTypeBreakers).ToList();
                    var freeSection = sections.Where(section => section.Breakers.BreakerList.Count == 1 &&
                                          section.Breakers.BreakerList.First().Label == ParserStrings.sectionBreakerListItemLabelFree).First();

                    if (freeSection != null) {
                        var freeBreakerListItem = freeSection.Breakers.BreakerList.First();

                        var newRecord = new FreeGameRecord() {
                            Title = freeBreakerListItem.Title,
                            Description = freeBreakerListItem.Description,
                            Disclaimer = freeBreakerListItem.Disclaimer,
                            Url = ParserStrings.giveawayUrl
                        };

                        _logger.LogInformation(ParserStrings.debugFoundInfo, newRecord.Description);
                        _logger.LogDebug(newRecord.Disclaimer);

                        parseResult.Records.Add(newRecord);

                        if (records.Count == 0 || !records.Any(record => record.Description == newRecord.Description && record.Disclaimer == newRecord.Disclaimer)) {
                            _logger.LogInformation(ParserStrings.debugFoundNewRecord, newRecord.Description);
                            parseResult.NotifyRecords.Add(new NotifyRecord(newRecord));
                        } else _logger.LogDebug(ParserStrings.debugFoundInPreviousRecord, newRecord.Description);
                    } else _logger.LogDebug(ParserStrings.debugNoFreeSection);
                } else _logger.LogDebug(ParserStrings.debugNoJsonData);

                _logger.LogDebug($"Done: {ParserStrings.debugParse}");
                return parseResult;
            } catch (Exception) {
                _logger.LogError($"Error: {ParserStrings.debugParse}");
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
