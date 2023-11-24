using QuantumHack.Models;

namespace QuantumHack.Arithmetic;

public static class Dijkstra
{
    public static List<Edge> FindOptimalEmissions(IEnumerable<Edge> edges, IEnumerable<Vertice> vertices)
    {
        return [];
    }

    public static List<Edge> FindOptimalCost(IEnumerable<Edge> edges, IEnumerable<Vertice> vertices)
    {
        return [];
    }

    public static List<Edge> FindOptimalTime(IEnumerable<Edge> edges, IEnumerable<Vertice> vertices)
    {
        return [];
    }

    public static List<Edge> FindBalancedOptimal(IEnumerable<Edge> edges, IEnumerable<Vertice> vertices)
    {
        return [];
    }

    private static List<Edge> DijkstrasAlgorithm(IList<Edge> edges, IList<Vertice> vertices, Vertice startPoint, OptimisationType optimisationType)
    {
       var unvisited = vertices;
       vertices.Remove(startPoint);
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
