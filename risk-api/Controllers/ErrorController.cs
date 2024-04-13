using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using risk_api.Helpers;

namespace risk_api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError() {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context.Error; // Your exception

        if (exception.GetType() == typeof(GenericClientException))//Return error message
        {
            var clientException = (GenericClientException)exception;
            return Problem(
                title: clientException.Message,
                statusCode: clientException.ErrorCode);
        }
            
        return Problem();
    }

    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        var exception = exceptionHandlerFeature.Error; 

        if (exception.GetType() == typeof(GenericClientException))
        {
            var clientException = (GenericClientException)exception;
            return Problem(
                title: clientException.Message,
                detail:exceptionHandlerFeature.Error.StackTrace,
                statusCode: clientException.ErrorCode);
        }

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }
}