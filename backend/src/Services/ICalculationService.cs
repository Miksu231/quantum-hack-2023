using QuantumHack.Models;
using QuantumHack.Services;

public interface ICalculationService
{
    public List<Edge> FindOptimalEmissions(Graph graph);
    public List<Edge> FindOptimalCost(Graph graph);
    public List<Edge> FindOptimalTime(Graph graph);
    public List<Edge> FindBalancedOptimal(Graph graph);
}