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
    public OptimalResult CalculateOptimalRoute([FromQuery(Name = "type")] string type = "Balanced")
    {
        if (Enum.TryParse<OptimisationType>(type, true, out var optimisationType))
        {
            return new OptimalResult(_calculationService.FindOptimalRoute(GetGraph(), optimisationType));
        }
        else
        {
            return new OptimalResult(_calculationService.FindOptimalRoute(GetGraph(), OptimisationType.Balanced));
        }

    }

}

