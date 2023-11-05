using System.Text.Json.Serialization;

namespace TailorMeWebApi.Models
{
    public class Sizes
    {
        [JsonPropertyName("SizeId")]
        public int SizeId { get; set; }

        [JsonPropertyName("ProductTypeId")]
        public int ProductTypeId { get; set; }

        [JsonPropertyName("BrandId")]
        public int BrandId { get; set; }

        [JsonPropertyName("SizeName")]
        public string? SizeName { get; set; }

        [JsonPropertyName("BrandName")]
        public string? BrandName { get; set; }

        [JsonPropertyName("ChestMaxMeasurement")]
        public int ChestMaxMeasurement { get; set; }

        [JsonPropertyName("ChestMinMeasurement")]
        public int ChestMinMeasurement { get; set; }

        [JsonPropertyName("WaistMaxMeasurement")]
        public int WaistMaxMeasurement { get; set; }

        [JsonPropertyName("WaistMinMeasurement")]
        public int WaistMinMeasurement { get; set; }

        [JsonPropertyName("HipMaxMeasurement")]
        public int HipMaxMeasurement { get; set; }

        [JsonPropertyName("HipMinMeasurement")]
        public int HipMinMeasurement { get; set; }
    }
}
