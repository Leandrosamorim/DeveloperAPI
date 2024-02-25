using System.Text.Json.Serialization;

namespace Tests.Contract.Providers.Middleware
{
    public class ProviderState
    {
        [JsonPropertyName("action")]
        public string? Action { get; set; }
        [JsonPropertyName("params")]
        public IDictionary<string, string>? Params { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }
    }
}
