using EGSMobileFreeGamesNotifier.Models.Record;

namespace EGSMobileFreeGamesNotifier.Models
{
    public class ParseResult
    {
        public List<FreeGameRecord> Records { get; set; } = [];

        public List<NotifyRecord> NotifyRecords { get; set; } = [];
    }
}
