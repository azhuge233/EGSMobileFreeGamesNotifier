namespace EGSMobileFreeGamesNotifier.Strings
{
    internal class JsonStrings
    {
        #region path strings
        internal static readonly string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}Config{Path.DirectorySeparatorChar}config.json";
        internal static readonly string recordsPath = $"{AppDomain.CurrentDomain.BaseDirectory}Records{Path.DirectorySeparatorChar}records.json";
        #endregion

        #region debug strings
        internal static readonly string debugWrite = "Write records";
        internal static readonly string debugLoadConfig = "Load config";
        internal static readonly string debugLoadRecords = "Load previous records";

        internal static readonly string debugDeleteRecord = "Deleting record: {0}";
        #endregion
    }
}
