using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public class Graph(List<Vertice> vertices, List<Edge> edges, Vertice startPoint, Vertice endPoint)
{
    [JsonPropertyName("vertices")]
    public List<Vertice> Vertices { get; init; } = vertices;
    [JsonPropertyName("edges")]
    public List<Edge> Edges { get; init; } = edges;
    [JsonPropertyName("startPoint")]
    public Vertice StartPoint { get; init; } = startPoint;
    [JsonPropertyName("endPoint")]
    public Vertice EndPoint { get; init; } = endPoint;

    public List<Vertice> FindVerticeNeighbours(Vertice vertice)
    {
        var neighbourIds = vertice.Edges.Select(x => x.DestinationId);
        return Vertices.FindAll(vertice => neighbourIds.Contains(vertice.Id));
    }
}