using System.Text.Json.Serialization;

namespace Business.Models
{
    public class Address
    {
        [JsonPropertyName("street")]
        public string Street { get; set; } = default!;

        [JsonPropertyName("suite")]
        public string Suite { get; set; } = default!;

        [JsonPropertyName("city")]
        public string City { get; set; } = default!;

        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; } = default!;

        [JsonPropertyName("geo")]
        public Geo Geo { get; set; } = default!;
    }
}
