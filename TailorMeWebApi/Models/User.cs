using System.Text.Json.Serialization;

namespace TailorMeWebApi.Models
{
    public class User
    {
        [JsonPropertyName("UserId")]
        public string? UserId { get; set; }

        [JsonPropertyName("Username")]
        public string? Username { get; set; }

        [JsonPropertyName("ChestMeasurement")]
        public float? ChestMeasurement { get; set; }

        [JsonPropertyName("WaistMeasurement")]
        public float? WaistMeasurement { get; set; }

        [JsonPropertyName("HipMeasurement")]
        public float? HipMeasurement { get; set; }

        public List<BrandSize>? Sizes { get; set; }
    }
}
