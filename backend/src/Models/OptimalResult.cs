using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public record OptimalResult(List<Edge> Path)
{
    [JsonPropertyName("path")]
    public List<Edge> Path { get; init; } = Path;
    [JsonPropertyName("cost")]
    public double Cost { get; init; } = Path.Select(x => x.Weight.Cost).Sum();
    [JsonPropertyName("emissions")]
    public double Emissions { get; init; } = Path.Select(x => x.Weight.Emissions).Sum();
    [JsonPropertyName("time")]
    public double Time { get; init; } = Path.Select(x => x.Weight.Time).Sum();
}