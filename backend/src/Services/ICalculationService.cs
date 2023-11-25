using QuantumHack.Models;

public interface ICalculationService
{
    public List<Edge> FindOptimalRoute(Graph graph, OptimisationType optimisationType);

    public Graph GetGraph();
}