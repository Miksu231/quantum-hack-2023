using QuantumHack.Models;

namespace QuantumHack.Services;

public interface ICalculationService
{
    public List<Edge> FindOptimalRoute(Graph graph, OptimisationType optimisationType);

    public Graph GetGraph(string continent);
}