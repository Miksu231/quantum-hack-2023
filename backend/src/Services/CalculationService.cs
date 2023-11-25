using QuantumHack.Algorithms;
using QuantumHack.Models;
using QuantumHack.Utils;

namespace QuantumHack.Services;

public class CalculationService : ICalculationService
{

    public Graph GetGraph()
    {
        return JsonReader.ReadGraphFromFile();;
    }

    public List<Edge> FindOptimalRoute(Graph graph, OptimisationType optimisationType)
    {
        List<Edge> optimalPath = [];
        Dijkstra.DijkstrasAlgorithm(graph, optimisationType);
        BuildOptimalRoute(graph, optimalPath, graph.EndPoint);
        optimalPath.Reverse();
        return optimalPath;
    }

    private static void BuildOptimalRoute(Graph graph, List<Edge> edges, Vertice vertice)
    {
        if (vertice.Id == graph.StartPoint.Id) return;
        var closestNeighbourToStart = graph.FindVerticeOriginNeighbours(vertice).MinBy(x => x.CostFromStart);
        edges.Add(closestNeighbourToStart!.Edges.Where(x => x.DestinationId == vertice.Id && x.IsOptimalEdge).First());
        BuildOptimalRoute(graph, edges, closestNeighbourToStart!);
    }
}