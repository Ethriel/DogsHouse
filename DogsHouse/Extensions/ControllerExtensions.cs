using DogsHouse.Services.Utility.ApiResult;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ActionResultByApiResult(this ControllerBase controller, IApiResult apiResult, ILogger logger)
        {
            switch (apiResult.ApiResultStatus)
            {
                case ApiResultStatus.Ok:
                    return controller.Ok(apiResult);
                case ApiResultStatus.NotFound:
                    logger.LogWarning(message: ((IApiErrorResult)apiResult).LoggerMessage);
                    return controller.NotFound(apiResult);
                case ApiResultStatus.Conflict:
                    logger.LogWarning(message: ((IApiErrorResult)apiResult).LoggerMessage);
                    return controller.Conflict(((IApiErrorResult)apiResult).Errors);
                case ApiResultStatus.NoContent:
                    logger.LogInformation(message: apiResult.Message);
                    return controller.NoContent();
                case ApiResultStatus.BadRequest:
                default:
                    logger.LogError(message: ((IApiErrorResult)apiResult).LoggerMessage);
                    return controller.BadRequest(((IApiErrorResult)apiResult).Errors);
            }
        }
    }
}
