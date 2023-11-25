using Microsoft.AspNetCore.Mvc;
using QuantumHack.Models;

namespace QuantumHack.Controllers;
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
public class QuantumHackController(ICalculationService calculationService) : ControllerBase
{
    private readonly ICalculationService _calculationService = calculationService;

    [HttpGet]
    public Graph GetGraph()
    {
        return _calculationService.GetGraph();
    }

    [HttpPost]
    public List<OptimalResult> CalculateOptimalRoute([FromQuery(Name = "type")] string type = "Balanced", [FromQuery(Name = "demand")] string demand = "0.0")
    {
        if (Enum.TryParse<OptimisationType>(type, true, out var optimisationType) && double.TryParse(demand, out var demandAmount))
        {
            return _calculationService.FindOptimalRoute(GetGraph(), optimisationType, demandAmount).Select(path => new OptimalResult(path.Item1, path.Item2)).ToList();
        }
        else
        {
            return _calculationService.FindOptimalRoute(GetGraph(), OptimisationType.Balanced, 0.0).Select(path => new OptimalResult(path.Item1, path.Item2)).ToList();;
        }

    }

}

