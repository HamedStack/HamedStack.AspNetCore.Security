// ReSharper disable UnusedType.Global

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HamedStack.AspNetCore.Security.Headers;

/// <summary>
/// Middleware to add default security headers to the HTTP response.
/// </summary>
public class DefaultSecurityHeaders
{
    private readonly RequestDelegate _next;
    private readonly SecurityHeaderSettings _settings;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultSecurityHeaders"/> class.
    /// </summary>
    /// <param name="next">The next request delegate in the pipeline.</param>
    /// <param name="settings">The security header settings.</param>
    /// <param name="logger">The logger instance.</param>
    public DefaultSecurityHeaders(RequestDelegate next, SecurityHeaderSettings settings, ILogger logger)
    {
        _next = next;
        _settings = settings;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The HTTP context for the request.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation("Adding security headers");

        // Add default headers
        context.Response.Headers["X-Frame-Options"] = _settings.XFrameOptions;
        context.Response.Headers["X-Permitted-Cross-Domain-Policies"] = _settings.XPermittedCrossDomainPolicies;
        context.Response.Headers["X-Xss-Protection"] = _settings.XXssProtection;
        context.Response.Headers["X-Content-Type-Options"] = _settings.XContentTypeOptions;
        context.Response.Headers["Referrer-Policy"] = _settings.ReferrerPolicy;
        context.Response.Headers["Permissions-Policy"] = _settings.PermissionsPolicy;
        context.Response.Headers["Content-Security-Policy"] = _settings.ContentSecurityPolicy;

        // Add custom headers
        foreach (var header in _settings.CustomHeaders)
        {
            context.Response.Headers[header.Key] = header.Value;
        }

        await _next(context);
    }
}