using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public class Graph(IList<Vertice> vertices, IList<Edge> edges)
{
    [JsonPropertyName("vertices")]
    public IList<Vertice> Vertices { get; init; } = vertices;
    [JsonPropertyName("edges")]
    public IList<Edge> Edges { get; init; } = edges;
}