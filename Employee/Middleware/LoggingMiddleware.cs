using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class LoggingMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;  // The next middleware in the pipeline
        _logger = logger;  // Logger instance for logging request details
    }

    // gets invoked for each HTTP request
    public async Task InvokeAsync(HttpContext context)
    {
        // Start a stopwatch 
        var stopwatch = Stopwatch.StartNew();

        // Call the next middleware in the pipeline
        await _next(context);

        // Stop the stopwatch after the request has been processed
        stopwatch.Stop();

        // Calculate the elapsed time in milliseconds
        var elapsedMs = stopwatch.ElapsedMilliseconds;

        // Log the details of the request and the time taken to process it
        _logger.LogInformation("Handled request {Method} {Url} in {ElapsedMilliseconds}ms",
            context.Request.Method,  
            context.Request.Path,  
            elapsedMs); 
    }
}
