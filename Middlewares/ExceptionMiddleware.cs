using HousingWebAPI.Errors;
using System.Net;

namespace HousingWebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment hostingEnvironment)
        {
            this.next = next;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                ApiError apiError;
                HttpStatusCode statusCode;
                string message = string.Empty;
                var exceptionType = ex.GetType();

                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = "You are not authorized";
                }

                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Some Unknown Error Occured";
                }

                if (hostingEnvironment.IsDevelopment())
                {
                    apiError = new ApiError((int)statusCode, ex.Message, ex.StackTrace.ToString());
                }
                else
                {
                    apiError = new ApiError((int)statusCode, message);
                }
                logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = (int)statusCode;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(apiError.ToString());
            }
        }
    }
}
