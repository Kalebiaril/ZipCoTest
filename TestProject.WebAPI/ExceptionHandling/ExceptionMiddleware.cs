using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using TestProject.WebAPI.Exceptions;

namespace TestProject.WebAPI.ExceptionHandling
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException notFoundException)
            {              
                await ProcessException(httpContext, notFoundException, HttpStatusCode.NotFound);
                return;
            }
            catch (BadRequestException badRequestException)
            {                
               await ProcessException(httpContext, badRequestException, HttpStatusCode.BadRequest);
               return;
            }
            catch (Exception exception)
            {               
                await ProcessException(httpContext, exception, HttpStatusCode.InternalServerError);
                return;
            }
        }

        private async Task ProcessException(HttpContext httpContext, Exception exception, HttpStatusCode statusCode)
        {
            if (httpContext.Response.HasStarted)
            {
                _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                return;
            }
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = @"text/plain";
            await httpContext.Response.WriteAsync(exception.Message);
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
