using EGSMobileFreeGamesNotifier.Models;
using EGSMobileFreeGamesNotifier.Models.EGSJsonData;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Parser(ILogger<Parser> logger) : IDisposable {
        private readonly ILogger<Parser> _logger = logger;

        public ParseResult Parse(Tuple<string, string> sources, List<FreeGameRecord> records) {
            try {
                _logger.LogDebug(ParserStrings.debugParse);

				var dict = new Dictionary<string, FreeGameRecord>();

                ParseSource(sources.Item1, "android", ref dict);
				ParseSource(sources.Item2, "ios", ref dict);

                var parseResult = new ParseResult { 
                    Records = [.. dict.Values]
				};

                foreach (var newRecord in parseResult.Records) {
                    if (!records.Any(record => record.SandboxID == newRecord.SandboxID)) {
                        _logger.LogDebug(ParserStrings.debugFoundNewRecord, newRecord.Title);
                        parseResult.NotifyRecords.Add(new NotifyRecord(newRecord));
                    } else _logger.LogDebug(ParserStrings.debugFoundInPreviousRecord, newRecord.Title);
                }

				_logger.LogDebug($"Done: {ParserStrings.debugParse}");
                return parseResult;
            } catch (Exception) {
                _logger.LogError($"Error: {ParserStrings.debugParse}");
                throw;
            } finally {
                Dispose();
            }
        }

        private void ParseSource(string source, string platform, ref Dictionary<string, FreeGameRecord> dict) {
            try {
                _logger.LogDebug(ParserStrings.debugParseSource);

				var jsonData = JsonSerializer.Deserialize<DataWrapper>(source);

                if (jsonData != null) {
                    var freeSection = jsonData.Data.Where(dataItem => dataItem.Type == ParserStrings.freeDataValue).First();

                    if (freeSection != null) { 
                        var offers = freeSection.Offers;

                        if (offers.Count > 0) {
                            foreach (var offer in offers) { 
                                var sandboxID = offer.SandboxId;
								var offerID = offer.OfferId;
								var title = offer.Content.Title;
								var slug = offer.Content.Mapping.Slug;

                                if (!dict.TryGetValue(sandboxID, out FreeGameRecord value)) {
                                    _logger.LogInformation(ParserStrings.debugFoundInfo, title);

                                    var newRecord = new FreeGameRecord {
                                        SandboxID = sandboxID,
                                        Title = title
                                    };

                                    if (platform == "android") {
                                        newRecord.OfferIDAndroid = offerID;
                                        newRecord.UrlAndroid = $"{ParserStrings.storeBaseUrl}{slug}";
                                        newRecord.PurchaseUrlAndroid = string.Format(ParserStrings.purchaseBaseUrl, sandboxID, offerID);
                                    } else { // just in case that iOS platform has exclusive games
                                        newRecord.OfferIDIOS = offerID;
                                        newRecord.UrlIOS = $"{ParserStrings.storeBaseUrl}{slug}";
                                        newRecord.PurchaseUrlIOS = string.Format(ParserStrings.purchaseBaseUrl, sandboxID, offerID);
                                    }

                                    dict.Add(sandboxID, newRecord);
                                } else {
									// if game present in the dictionary, means Android platform has the same game
                                    // bacause Android source was parsed first
                                    // updating the ios links
									_logger.LogInformation(ParserStrings.debugInfoInAndroid, title);

									value.OfferIDIOS = offerID;
									value.UrlIOS = $"{ParserStrings.storeBaseUrl}{slug}";
									value.PurchaseUrlIOS = string.Format(ParserStrings.purchaseBaseUrl, sandboxID, offerID);
                                }
							}
                        } else _logger.LogDebug(ParserStrings.debugNoFreeOffers);
					} else _logger.LogDebug(ParserStrings.debugNoFreeSection);
				} else _logger.LogDebug(ParserStrings.debugNoJsonData);

				_logger.LogDebug($"Done: {ParserStrings.debugParseSource}");
            } catch (Exception) {
                _logger.LogError($"Error: {ParserStrings.debugParseSource}");
                throw;
            }
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
