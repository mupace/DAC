using DAC.Constants.enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DAC.Extensions;

public static class OperationResultExtensions
{
    public static IActionResult ToActionResult(this OperationResult result, ControllerBase controller, string message = null)
    {
        switch (result)
        {
            case OperationResult.NotFound:
                return controller.NotFound(message);
            case OperationResult.Done:
                return controller.Ok();
            case OperationResult.Error:
            default:
                return controller.Problem(statusCode: StatusCodes.Status500InternalServerError, detail: message);
        }
    }
}