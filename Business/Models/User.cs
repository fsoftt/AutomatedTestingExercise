using System.Text.Json.Serialization;

namespace Business.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("username")]
        public string Username { get; set; } = default!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        [JsonPropertyName("address")]
        public Address Address { get; set; } = default!;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = default!;

        [JsonPropertyName("website")]
        public string Website { get; set; } = default!;

        [JsonPropertyName("company")]
        public Company Company { get; set; } = default!;
    }
}
