using System.Reflection.Metadata;
using QuantumHack.Constants;
using QuantumHack.Models;

namespace QuantumHack.Arithmetic;

public class Dijkstra(Vertice start, Vertice end, List<Vertice> vertices)
{
    private Vertice Start = start;
    private Vertice End = end;
    private List<Vertice> Vertices = vertices;
    public static List<Edge> FindOptimalEmissions()
    {
        return [];
    }

    public static List<Edge> FindOptimalCost()
    {
        return [];
    }

    public static List<Edge> FindOptimalTime()
    {
        return [];
    }

    public static List<Edge> FindBalancedOptimal(Graph graph)
    {
        return [];
    }

    private static List<Vertice> DijkstrasAlgorithm(Graph graph, OptimisationType optimisationType)
    {
        graph.StartPoint.CostFromStart = 0;
        List<Vertice> priorityQueue = [ graph.StartPoint ];
        while(priorityQueue.Count > 0)
        {
            priorityQueue = [.. priorityQueue.OrderBy(x => x.CostFromStart)];
            var vertice = priorityQueue.First();
            vertice.Visited = true;
            priorityQueue.Remove(vertice);
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
                    dest.CostFromStart = meaningfulWeight;
                    foreach (var originalEdge in vertice.Edges)
                    {
                        if (originalEdge.DestinationId == edge.DestinationId)
                        {
                            originalEdge.IsOptimalEdge = false;
                        }
                    }
                    edge.IsOptimalEdge = true;
                }
            }
        }
        return []; 
    }

    private enum OptimisationType
    {
        Emissions,
        Cost,
        Time,
        Balanced


    }
}
