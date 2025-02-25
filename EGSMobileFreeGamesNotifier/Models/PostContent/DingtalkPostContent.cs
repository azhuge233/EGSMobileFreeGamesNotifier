using System.Text.Json.Serialization;

namespace EGSMobileFreeGamesNotifier.Models.PostContent
{
    public class Content
    {
        [JsonPropertyName("content")]
        public string Content_ { get; set; }
    }

    public class DingTalkPostContent
    {
        [JsonPropertyName("msgtype")]
        public string MessageType { get; set; } = "text";
        [JsonPropertyName("text")]
        public Content Text { get; set; } = new Content();
    }
}
