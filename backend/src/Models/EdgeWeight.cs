using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public record EdgeWeight
{
    [JsonPropertyName("emissions")]
    public double Emissions { get; init; }
    [JsonPropertyName("cost")]
    public double Cost { get; init; }
    [JsonPropertyName("time")]
    public double Time { get; init; }
}
