using System.Text.Json;
using EGSMobileFreeGamesNotifier.Models.Record;
using EGSMobileFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace EGSMobileFreeGamesNotifier.Services {
    internal class Json(ILogger<Json> logger) : IDisposable {
        private readonly ILogger<Json> _logger = logger;

        internal void WriteData(List<FreeGameRecord> data) {
            try {
                if (data.Count > 0) {
                    _logger.LogDebug(JsonStrings.debugWrite);

                    string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(JsonStrings.recordsPath, string.Empty);
                    File.WriteAllText(JsonStrings.recordsPath, json);

                    _logger.LogDebug($"Done: {JsonStrings.debugWrite}");
                } else _logger.LogDebug("No records detected, quit writing records");
            } catch (Exception) {
                _logger.LogError($"Error: {JsonStrings.debugWrite}");
                throw;
            } finally {
                Dispose();
            }
        }

        internal List<FreeGameRecord> LoadData() {
            try {
                _logger.LogDebug(JsonStrings.debugLoadRecords);

                var content = JsonSerializer.Deserialize<List<FreeGameRecord>>(File.ReadAllText(JsonStrings.recordsPath));

                _logger.LogDebug($"Done: {JsonStrings.debugLoadRecords}");
                return content;
            } catch (Exception) {
                _logger.LogError($"Error: {JsonStrings.debugLoadRecords}");
                throw;
            }
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
