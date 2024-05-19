using System.Text.Json;

namespace HousingWebAPI.Errors
{
    public class ApiError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }

        public ApiError(int errorCode, string errorMessage, string? errorDetails = null)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.ErrorDetails = errorDetails;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
