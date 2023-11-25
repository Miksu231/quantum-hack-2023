using System.Text.Json;
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

    public List<ValueTuple<List<Edge>, double>> FindOptimalRoute(Graph graph, OptimisationType optimisationType, double demand)
    {
        List<ValueTuple<List<Edge>, double>> optimalPaths = [];
        var modifiedGraph = graph;
        while(demand > 0)
        {
            List<Edge> optimalPath = [];
            Dijkstra.DijkstrasAlgorithm(modifiedGraph, optimisationType);
            BuildOptimalRoute(modifiedGraph, optimalPath, modifiedGraph.EndPoint);
            optimalPath.Reverse();
            var minCapacity = optimalPath.MinBy(x => x.Capacity);
            if (demand > minCapacity!.Capacity)
            {
                foreach (var edge in optimalPath)
                {
                        modifiedGraph.AllVertices.SelectMany(x => x.Edges).Where(x => x.Id == edge.Id).First().Capacity -= minCapacity!.Capacity;
                }
                demand -= minCapacity!.Capacity;
                optimalPaths.Add(new (optimalPath, minCapacity.Capacity));
            }
            else
            {
                optimalPaths.Add(new (optimalPath, demand));
                demand = 0;
                return optimalPaths;
            }
        }
        return optimalPaths;
    }

    private static void BuildOptimalRoute(Graph graph, List<Edge> edges, Vertice vertice)
    {
        if (vertice.Id == graph.StartPoint.Id) return;
        var closestNeighbours = graph.FindVerticeOriginNeighbours(vertice).OrderBy(x => x.CostFromStart);
        var closestPossibleNeighbour = closestNeighbours.First(x => x.Edges.Exists(x => x.DestinationId == vertice.Id && x.IsOptimalEdge && x.Capacity > 0));
        edges.Add(closestPossibleNeighbour!.Edges.Where(x => x.DestinationId == vertice.Id && x.IsOptimalEdge).First());
        BuildOptimalRoute(graph, edges, closestPossibleNeighbour);
    }
}