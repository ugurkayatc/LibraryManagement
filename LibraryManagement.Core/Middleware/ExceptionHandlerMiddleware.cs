using LibraryManagement.Core.Exception;
using LibraryManagement.Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LibraryManagement.Core.Middleware
{
    // Middleware for handling exceptions and returning appropriate responses.
    public class ExceptionHandlerMiddleware
    {
        readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        // Initializes a new instance of the ExceptionHandlerMiddleware class.
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Invokes the middleware.
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                // Handles CustomException and returns the response with errors.
                var response = new BaseResponse<object>();
                response.Errors = ex.Errors;
                response.Status = ErrorHandling.Error;
                response.Data = default;

                if (!(httpContext.Response.StatusCode >= 400 && httpContext.Response.StatusCode < 500))
                {
                    httpContext.Response.StatusCode = 406;
                }

                httpContext.Response.ContentType = "application/json";
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                };
                await httpContext.Response.WriteAsync(
                    JsonConvert.SerializeObject(response,
                        new JsonSerializerSettings
                        {
                            ContractResolver = contractResolver,
                        }));

                _logger.LogInformation($"Error: {ex.Errors}");
            }
            catch (BadRequestException ex)
            {
                // Handles BadRequestException and returns the response with errors.
                var response = new BaseResponse<object>
                {
                    Errors = ex.Errors,
                    Status = ErrorHandling.Error,
                    Data = default
                };

                if (httpContext.Response.StatusCode is < 400 or >= 500)
                {
                    httpContext.Response.StatusCode = 406;
                }

                httpContext.Response.ContentType = "application/json";
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                };
                await httpContext.Response.WriteAsync(
                    JsonConvert.SerializeObject(response,
                        new JsonSerializerSettings
                        {
                            ContractResolver = contractResolver,
                        }));

                _logger.LogInformation($"Error: {response.Errors}");
            }
        }
    }
}
