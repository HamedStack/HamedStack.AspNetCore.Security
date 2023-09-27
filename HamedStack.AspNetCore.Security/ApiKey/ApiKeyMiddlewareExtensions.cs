// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Builder;

namespace HamedStack.AspNetCore.Security.ApiKey;

/// <summary>
/// Extension methods for the IApplicationBuilder.
/// </summary>
public static class ApiKeyMiddlewareExtensions
{
    /// <summary>
    /// Adds the ApiKeyMiddleware to the middleware pipeline.
    /// Make sure to provide the API key in the HTTP request headers with the key name 'x-api-key' 
    /// or as a query string parameter named 'x-api-key'.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <param name="configureOptions">A delegate to configure the middleware options.</param>
    /// <returns>The application builder with the middleware added.</returns>
    public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder, Action<ApiKeyOption> configureOptions)
    {
        var options = new ApiKeyOption();
        configureOptions(options);
        return builder.UseMiddleware<ApiKeyMiddleware>(options);
    }
}