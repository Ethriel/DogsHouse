using Newtonsoft.Json;

namespace DogsHouse.Services.Utility.ApiResult
{
    public interface IApiErrorResult : IApiResult
    {
        IEnumerable<string> Errors { get; set; }
        [JsonIgnore]
        string LoggerMessage { get; set; }

        void SetErrorResult(ApiResultStatus apiResultStatus = ApiResultStatus.BadRequest, string loggerMessage = "API request failed", string message = null, IEnumerable<string> errors = null);
    }
}
