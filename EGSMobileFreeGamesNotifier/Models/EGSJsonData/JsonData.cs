using System.Text.Json.Serialization;

namespace EGSMobileFreeGamesNotifier.Models.EGSJsonData {
	public class DataWrapper {
		[JsonPropertyName("data")]
		public List<DataItem> Data { get; set; } = [];
	}

	public class DataItem {
		[JsonPropertyName("offers")]
		public List<Offer> Offers { get; set; } = [];

		[JsonPropertyName("type")]
		public string Type { get; set; } = string.Empty;

		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;
	}

	public class Offer {
		[JsonPropertyName("content")]
		public Content Content { get; set; } = new Content();

		[JsonPropertyName("offerId")]
		public string OfferId { get; set; } = string.Empty;

		[JsonPropertyName("sandboxId")]
		public string SandboxId { get; set; } = string.Empty;
	}

	public class Content {
		[JsonPropertyName("mapping")]
		public Mapping Mapping { get; set; } = new Mapping();

		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;
	}

	public class Mapping {
		[JsonPropertyName("slug")]
		public string Slug { get; set; } = string.Empty;
	}
}
