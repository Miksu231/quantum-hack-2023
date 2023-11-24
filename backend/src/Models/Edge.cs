using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public record Edge
{
    [JsonPropertyName("originId")]
    public int OriginId { get; init; }
    [JsonPropertyName("destinationId")]
    public int DestinationId { get; init; }
    [JsonPropertyName("weight")]
    public EdgeWeight Weight { get; init; } = new();
}
