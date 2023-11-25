using QuantumHack.Algorithms;
using QuantumHack.Models;
using QuantumHack.Utils;

namespace QuantumHack.Services;

public class CalculationService: ICalculationService
{
    private readonly Graph _graph = JsonReader.ReadGraphFromFile();

    public Graph GetGraph()
    {
        return _graph;
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
        var closestNeighbourToStart = graph.FindVerticeNeighbours(vertice).MinBy(x => x.CostFromStart);
        edges.Add(closestNeighbourToStart!.Edges.Where(x => x.DestinationId == vertice.Id && x.IsOptimalEdge).Single());
        BuildOptimalRoute(graph, edges, closestNeighbourToStart!);
    }
}