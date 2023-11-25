using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public record Edge
{
    [JsonPropertyName("id")]
    public int Id { get; init; }
    [JsonPropertyName("originId")]
    public int OriginId { get; init; }
    [JsonPropertyName("destinationId")]
    public int DestinationId { get; init; }
    [JsonPropertyName("weight")]
    public EdgeWeight Weight { get; init; } = new();
    [JsonPropertyName("capacity")]
    public double Capacity { get; set; }
    [JsonIgnore]
    public bool IsOptimalEdge { get; set; } = false;
}
