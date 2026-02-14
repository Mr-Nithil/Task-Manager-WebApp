using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next, 
            ILogger<GlobalExceptionHandlerMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var statusCode = exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var errorResponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = _env.IsDevelopment() 
                    ? exception.Message 
                    : "An error occurred. Please try again later.",
                Details = _env.IsDevelopment() ? exception.StackTrace : null,
                Timestamp = DateTime.UtcNow,
                Path = context.Request.Path
            };

            var options = new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(errorResponse, options)
            );
        }
    }
}