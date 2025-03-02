﻿using System.Text.Json.Serialization;

namespace EGSMobileFreeGamesNotifier.Models.PostContent {
	public class QQHttpPostContent {
		[JsonPropertyName("user_id")]
		public string UserID { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
