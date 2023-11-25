using QuantumHack.Models;

public interface ICalculationService
{
    public List<ValueTuple<List<Edge>, double>> FindOptimalRoute(Graph graph, OptimisationType optimisationType, double demand);

    public Graph GetGraph();
}