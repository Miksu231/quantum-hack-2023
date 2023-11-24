using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public record Vertice
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
    [JsonPropertyName("capacity")]
    public double Capacity { get; init; }
    [JsonPropertyName("id")]
    public int Id { get; init; }
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
    [JsonPropertyName("edges")]
    public List<Edge> Edges { get; init; } = [];
    [JsonPropertyName("costFromStart")]
    public double CostFromStart { get; set; } = double.PositiveInfinity;
    [JsonPropertyName("visited")]
    public bool Visited { get; set; } = false;
}