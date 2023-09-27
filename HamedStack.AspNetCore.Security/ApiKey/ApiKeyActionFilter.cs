// ReSharper disable UnusedType.Global

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HamedStack.AspNetCore.Security.ApiKey;

/// <summary>
/// Provides an action filter that verifies the presence and validity of an API key in the request.
/// The API key, named 'x-api-key', can be provided either as a header or a query string parameter.
/// </summary>
public class ApiKeyActionFilter : IActionFilter
{
    private const string API_KEY_NAME = "x-api-key";
    private readonly ILogger<ApiKeyActionFilter> _logger;
    private readonly ApiKeyOption _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeyActionFilter"/> class.
    /// </summary>
    /// <param name="logger">The logger used to log warning messages.</param>
    /// <param name="options">The API key options.</param>
    public ApiKeyActionFilter(ILogger<ApiKeyActionFilter> logger, ApiKeyOption options)
    {
        _logger = logger;
        _options = options;
    }

    /// <summary>
    /// Invoked before the action method is executed. This method checks for the presence of the 'x-api-key'
    /// in either the request header or query string and verifies its validity.
    /// </summary>
    /// <param name="context">The action executing context.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var apiKeyFromHeader = context.HttpContext.Request.Headers[API_KEY_NAME].FirstOrDefault();
        var apiKeyFromQueryString = context.HttpContext.Request.Query[API_KEY_NAME].ToString();

        var extractedApiKey = apiKeyFromHeader ?? apiKeyFromQueryString;

        if (string.IsNullOrEmpty(extractedApiKey))
        {
            _logger.LogWarning("API key was not provided.");
            SetProblemDetail(context, 401, "API key was not provided.");
            return;
        }

        if (extractedApiKey != _options.ApiKey)
        {
            _logger.LogWarning($"Invalid API key provided.");
            SetProblemDetail(context, 401, "Invalid API key.");
            return;
        }

        if (_options.ExpiryInDays.HasValue)
        {
            var apiKeyExpiryDate = DateTime.Now.AddDays(_options.ExpiryInDays.Value);
            if (apiKeyExpiryDate < DateTime.Now)
            {
                _logger.LogWarning($"API key has expired.");
                SetProblemDetail(context, 401, "API key has expired.");
            }
        }
    }

    /// <summary>
    /// Invoked after the action method is executed. This method currently does nothing post-action execution.
    /// </summary>
    /// <param name="context">The action executed context.</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action taken after the action method execution.
    }

    /// <summary>
    /// Sets a problem detail to the action executing context based on the provided parameters.
    /// </summary>
    /// <param name="context">The action executing context.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="detail">The detail of the problem.</param>
    private static void SetProblemDetail(ActionExecutingContext context, int statusCode, string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "API Key Verification Failed",
            Detail = detail
        };

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }
}