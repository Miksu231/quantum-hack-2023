using QuantumHack.Constants;
using QuantumHack.Models;

namespace QuantumHack.Algorithms;

public static class Dijkstra
{
    public static void DijkstrasAlgorithm(Graph graph, OptimisationType optimisationType)
    {
        graph.StartPoint.CostFromStart = 0;
        List<Vertice> priorityQueue = [graph.StartPoint];
        List<Vertice> visitedVertices = [];
        while (priorityQueue.Count > 0)
        {
            priorityQueue = [.. priorityQueue.OrderBy(x => x.CostFromStart)];
            var vertice = priorityQueue.First();
            var verticeNeighbours = graph.FindVerticeNeighbours(vertice);
            vertice.Visited = true;
            priorityQueue.Remove(vertice);
            if (vertice.Id == graph.EndPoint.Id)
            {
                graph.EndPoint.CostFromStart = vertice.CostFromStart;
                graph.EndPoint.Visited = true;
            }
            if (vertice.Id != graph.StartPoint.Id)
            {
                visitedVertices.Add(vertice);
            }
            foreach (var edge in vertice.Edges)
            {
                var dest = verticeNeighbours.Find(x => x.Id.Equals(edge.DestinationId));
                var meaningfulWeight =
                    optimisationType == OptimisationType.Emissions ? edge.Weight.Emissions
                    : optimisationType == OptimisationType.Cost ? edge.Weight.Cost
                    : optimisationType == OptimisationType.Time ? edge.Weight.Time
                    : FactorWeightingConstants.TIME * Math.Pow(edge.Weight.Time, 2)
                        + FactorWeightingConstants.EMISSIONS * Math.Pow(edge.Weight.Emissions, 2)
                        + FactorWeightingConstants.COST * Math.Pow(edge.Weight.Cost, 2);
                if (dest!.CostFromStart == double.MaxValue || dest.CostFromStart > vertice.CostFromStart + meaningfulWeight)
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
}
