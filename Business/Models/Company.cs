using System.Text.Json.Serialization;

namespace Business.Models
{
    public class Company
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("catchphrase")]
        public string CatchPhrase { get; set; } = default!;

        [JsonPropertyName("bs")]
        public string BusinessName { get; set; } = default!;
    }
}
