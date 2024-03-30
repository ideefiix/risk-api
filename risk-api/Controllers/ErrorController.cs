using Microsoft.AspNetCore.Mvc;

namespace risk_api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError() =>
        Problem();
}