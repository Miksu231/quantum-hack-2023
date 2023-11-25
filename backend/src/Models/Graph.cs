using System.Text.Json.Serialization;

namespace QuantumHack.Models;

public class Graph(List<Vertice> vertices, Vertice startPoint, Vertice endPoint)
{
    [JsonPropertyName("vertices")]
    public List<Vertice> Vertices { get; set; } = vertices;
    [JsonPropertyName("startPoint")]
    public Vertice StartPoint { get; init; } = startPoint;
    [JsonPropertyName("endPoint")]
    public Vertice EndPoint { get; init; } = endPoint;
    [JsonIgnore]
    public List<Vertice> AllVertices { 
        get  => Vertices.Concat([StartPoint, EndPoint]).ToList();
    }

    public List<Vertice> FindVerticeNeighbours(Vertice vertice)
    {
        var neighbourIds = vertice.Edges.Select(x => x.DestinationId);
        var allVertices = Vertices.Concat([StartPoint, EndPoint]);
        return allVertices.Where(vertice => neighbourIds.Contains(vertice.Id)).ToList();
    }

    public List<Vertice> FindVerticeOriginNeighbours(Vertice vertice)
    {
        var allVertices = Vertices.Concat([StartPoint, EndPoint]);
        return allVertices.Where(currentVertice => currentVertice.Edges.Any(edge => edge.DestinationId == vertice.Id)).ToList();
    }

    public void RemoveEdge(int id)
    {
        Vertices.ForEach(x => x.Edges.RemoveAll(x => x.Id == id));
    }
}