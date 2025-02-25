using System.Text.Json.Serialization;

namespace EGSMobileFreeGamesNotifier.Models.EGSJsonData {
	public class LayoutWrapper {
		[JsonPropertyName("layout")]
		public Layout Layout { get; set; } = new Layout();
	}

	public class Layout {
		[JsonPropertyName("section")]
		public List<Section> Sections { get; set; } = [];
	}

	public class Section {
		[JsonPropertyName("moduleType")]
		public string ModuleType { get; set; } = string.Empty;

		[JsonPropertyName("_type")]
		public string Type { get; set; } = string.Empty;

		[JsonPropertyName("breakers")]
		public Breakers Breakers { get; set; } = new Breakers(); // 非可空，始终初始化
	}

	public class Breakers {
		[JsonPropertyName("_type")]
		public string Type { get; set; } = string.Empty;

		[JsonPropertyName("breakerList")]
		public List<Breaker> BreakerList { get; set; } = [];
	}

	public class Breaker {
		[JsonPropertyName("backgroundImage")]
		public string BackgroundImage { get; set; } = string.Empty;

		[JsonPropertyName("_type")]
		public string Type { get; set; } = string.Empty;

		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;

		[JsonPropertyName("label")]
		public string Label { get; set; } = string.Empty;

		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;

		[JsonPropertyName("disclaimer")]
		public string Disclaimer { get; set; } = string.Empty;
	}
}
