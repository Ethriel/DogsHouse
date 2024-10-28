using Newtonsoft.Json;

namespace DogsHouse.Services.Utility.ApiResult
{
    public class ApiErrorResult : ApiResult, IApiErrorResult
    {
        public IEnumerable<string> Errors { get; set; }
        [JsonIgnore]
        public string LoggerMessage { get; set; }

        public ApiErrorResult() { }

        public ApiErrorResult(ApiResultStatus apiResultStatus = ApiResultStatus.BadRequest, string loggerErrorMessage = "API request failed", string errorMessage = "An error occured", IEnumerable<string> errors = null)
        {
            SetErrorResult(apiResultStatus, loggerErrorMessage, errorMessage, errors);
        }

        public void SetErrorResult(ApiResultStatus apiResultStatus = ApiResultStatus.BadRequest, string loggerMessage = "API request failed", string errorMessage = "An error occured", IEnumerable<string> errors = null)
        {
            ApiResultStatus = apiResultStatus;
            LoggerMessage = loggerMessage;
            Message = errorMessage;
            Errors = errors;
        }
    }
}
