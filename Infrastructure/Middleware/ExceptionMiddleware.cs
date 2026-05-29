using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PetConnect.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next { get;  }

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (InvalidNameException)
            {
                httpContext.Response.ContentType = "Application Problem + Json";
                httpContext.Response.StatusCode = 400;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = string.Empty,
                    Title = "Name is Invalid",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await httpContext.Response.WriteAsync(problemDetailsJson);
            }

            catch (Exception ex)
            {
                httpContext.Response.ContentType = "Application Problem + Json";
                httpContext.Response.StatusCode = 500;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = string.Empty,
                    Title = "Internal Server Error - something went wrong.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await httpContext.Response.WriteAsync(problemDetailsJson);
            }


        }
    }
}
