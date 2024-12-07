using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.api.Errors;

namespace Talabat.api.Midlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                //take action with req  
                await _next.Invoke(httpContext); // go to 
                // teak action with res

            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var apiBehavior = _env.IsDevelopment()? 
                                new ApiExceptionResponse(httpContext.Response.StatusCode,ex.StackTrace,ex.Message)
                                : new ApiExceptionResponse(httpContext.Response.StatusCode);



                var srializtion = JsonSerializer.Serialize(apiBehavior);


                await httpContext.Response.WriteAsync(srializtion);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class ExceptionMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ExceptionMiddleware>();
    //    }
    //}
}
