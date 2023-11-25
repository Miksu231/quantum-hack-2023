using Microsoft.AspNetCore.Mvc;
using QuantumHack.Models;
using QuantumHack.Services;

namespace QuantumHack.Controllers;
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
public class QuantumHackController(ICalculationService calculationService) : ControllerBase
{
    private readonly ICalculationService _calculationService = calculationService;

    [HttpGet("{continent}")]
    public Graph GetGraph(string continent)
    {
        return _calculationService.GetGraph(continent);
    }

    [HttpPost("{continent}")]
    public OptimalResult CalculateOptimalRoute(string continent, [FromQuery(Name = "type")] string type = "Balanced")
    {
        if (Enum.TryParse<OptimisationType>(type, true, out var optimisationType))
        {
            return new OptimalResult(_calculationService.FindOptimalRoute(GetGraph(continent), optimisationType));
        }
        else
        {
            return new OptimalResult(_calculationService.FindOptimalRoute(GetGraph(continent), OptimisationType.Balanced));
        }

    }

}

