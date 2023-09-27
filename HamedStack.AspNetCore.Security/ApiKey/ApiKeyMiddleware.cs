// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HamedStack.AspNetCore.Security.ApiKey;

/// <summary>
/// Middleware to handle API key authentication.
/// This middleware expects an API key to be provided either in the HTTP request headers with the key name 'x-api-key' 
/// or as a query string parameter named 'x-api-key'.
/// </summary>
public class ApiKeyMiddleware
{
    private const string API_KEY_NAME = "x-api-key";
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiKeyMiddleware> _logger;
    private readonly ApiKeyOption _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeyMiddleware"/> class.
    /// </summary>
    /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
    /// <param name="logger">The logger used to log information and exceptions.</param>
    /// <param name="options">The options configuration for the API key middleware.</param>
    public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger, ApiKeyOption options)
    {
        _next = next;
        _logger = logger;
        _options = options;
    }

    /// <summary>
    /// Invokes the middleware to process the API key authentication.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var apiKeyFromHeader = context.Request.Headers[API_KEY_NAME].FirstOrDefault();
        var apiKeyFromQueryString = context.Request.Query[API_KEY_NAME].ToString();

        var extractedApiKey = apiKeyFromHeader ?? apiKeyFromQueryString;

        if (string.IsNullOrEmpty(extractedApiKey))
        {
            _logger.LogWarning("API key was not provided.");
            await WriteProblemDetail(context, 401, "API key was not provided.");
            return;
        }

        if (extractedApiKey != _options.ApiKey)
        {
            _logger.LogWarning($"Invalid API key provided.");
            await WriteProblemDetail(context, 401, "Invalid API key.");
            return;
        }

        if (_options.ExpiryInDays.HasValue)
        {
            var apiKeyExpiryDate = DateTime.Now.AddDays(_options.ExpiryInDays.Value);
            if (apiKeyExpiryDate < DateTime.Now)
            {
                _logger.LogWarning($"API key has expired.");
                await WriteProblemDetail(context, 401, "API key has expired.");
                return;
            }
        }

        await _next(context);
    }

    /// <summary>
    /// Writes a problem detail response when API key verification fails.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <param name="statusCode">The HTTP status code for the response.</param>
    /// <param name="detail">The detailed error message.</param>
    private static async Task WriteProblemDetail(HttpContext context, int statusCode, string detail)
    {
        context.Response.StatusCode = statusCode;
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "API Key Verification Failed",
            Detail = detail
        };
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}