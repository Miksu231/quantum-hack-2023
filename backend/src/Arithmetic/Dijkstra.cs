using QuantumHack.Constants;
using QuantumHack.Models;

namespace QuantumHack.Arithmetic;

public static class Dijkstra
{
    public static List<Edge> FindOptimalEmissions(Graph graph)
    {
        List<Edge> optimalPath = [];
        DijkstrasAlgorithm(graph, OptimisationType.Emissions);
        BuildOptimalRoute(graph, optimalPath, graph.EndPoint);
        optimalPath.Reverse();
        return optimalPath;
    }

    public static List<Edge> FindOptimalCost(Graph graph)
    {
        List<Edge> optimalPath = [];
        DijkstrasAlgorithm(graph, OptimisationType.Cost);
        BuildOptimalRoute(graph, optimalPath, graph.EndPoint);
        optimalPath.Reverse();
        return optimalPath;
    }

    public static List<Edge> FindOptimalTime(Graph graph)
    {
        List<Edge> optimalPath = [];
        DijkstrasAlgorithm(graph, OptimisationType.Time);
        BuildOptimalRoute(graph, optimalPath, graph.EndPoint);
        optimalPath.Reverse();
        return optimalPath;
    }

    public static List<Edge> FindBalancedOptimal(Graph graph)
    {
        List<Edge> optimalPath = [];
        DijkstrasAlgorithm(graph, OptimisationType.Balanced);
        BuildOptimalRoute(graph, optimalPath, graph.EndPoint);
        optimalPath.Reverse();
        return optimalPath;
    }

    private static void DijkstrasAlgorithm(Graph graph, OptimisationType optimisationType)
    {
        graph.StartPoint.CostFromStart = 0;
        List<Vertice> priorityQueue = [ graph.StartPoint ];
        List<Vertice> visitedVertices = [];
        while(priorityQueue.Count > 0)
        {
            priorityQueue = [.. priorityQueue.OrderBy(x => x.CostFromStart)];
            var vertice = priorityQueue.First();
            vertice.Visited = true;
            priorityQueue.Remove(vertice);
            if(vertice.Id == graph.EndPoint.Id)
            {
                graph.EndPoint.CostFromStart = vertice.CostFromStart;
                graph.EndPoint.Visited = true;
                break;
            }
            visitedVertices.Add(vertice);
            foreach(var edge in vertice.Edges)
            {
                var dest = priorityQueue.Find(x => x.Id.Equals(edge.DestinationId));
                var meaningfulWeight = 
                    optimisationType == OptimisationType.Emissions ? edge.Weight.Emissions
                    : optimisationType == OptimisationType.Cost ? edge.Weight.Cost
                    : optimisationType == OptimisationType.Time ? edge.Weight.Time
                    : FactorWeightingConstants.TIME * Math.Pow(edge.Weight.Time, 2)
                        + FactorWeightingConstants.EMISSIONS * Math.Pow(edge.Weight.Emissions, 2)
                        + FactorWeightingConstants.COST * Math.Pow(edge.Weight.Cost, 2);
                if (dest!.CostFromStart == double.PositiveInfinity || dest.CostFromStart > vertice.CostFromStart + meaningfulWeight)
                {
                    dest.CostFromStart = vertice.CostFromStart + meaningfulWeight;
                    foreach (var originalEdge in vertice.Edges)
                    {
                        if (originalEdge.DestinationId == edge.DestinationId)
                        {
                            originalEdge.IsOptimalEdge = false;
                        }
                    }
                    edge.IsOptimalEdge = true;
                }
                if (!dest.Visited)
                {
                    priorityQueue.Add(dest);
                }
            }
        }
        graph.Vertices = visitedVertices;
    }

    private static void BuildOptimalRoute(Graph graph, List<Edge> edges, Vertice vertice)
    {
        if (vertice.Id == graph.StartPoint.Id) return;
        var closestNeighbourToStart = graph.FindVerticeNeighbours(vertice).MinBy(x => x.CostFromStart);
        edges.Add(vertice.Edges.Where(x => x.DestinationId == closestNeighbourToStart!.Id && x.IsOptimalEdge).Single());
        BuildOptimalRoute(graph, edges, closestNeighbourToStart!);
    }

    private enum OptimisationType
    {
        Emissions,
        Cost,
        Time,
        Balanced


    }
}
