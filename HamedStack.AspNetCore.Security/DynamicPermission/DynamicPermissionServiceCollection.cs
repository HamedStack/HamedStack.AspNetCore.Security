// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace HamedStack.AspNetCore.Security.DynamicPermission;

/// <summary>
/// Provides extension methods for configuring dynamic permission-related services in the service collection.
/// </summary>
public static class DynamicPermissionServiceCollection
{
    /// <summary>
    /// Adds services required for dynamic permission-based authorization.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="cacheOptions">The caching options. If not provided, defaults to 60 minutes.</param>
    /// <returns>The service collection with added dynamic permission services.</returns>
    /// <remarks>
    /// This method adds the necessary services to enable dynamic permission-based authorization in the application.
    /// It configures the authorization policy provider and authorization handler required for handling custom permissions.
    /// The cache duration defaults to 60 minutes if not specified.
    /// </remarks>
    /// <example>
    /// <code>
    /// var cacheOptions = new AuthorizationCacheOptions
    /// {
    ///     DefaultCacheDuration = TimeSpan.FromMinutes(15)
    /// };
    /// services.AddDynamicPermission(cacheOptions);
    /// </code>
    /// <code>
    /// // Using default cache duration of 60 minutes.
    /// services.AddDynamicPermission();
    /// </code>
    /// </example>
    public static IServiceCollection AddDynamicPermission(this IServiceCollection services, AuthorizationCacheOptions? cacheOptions = null)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddMemoryCache();
        services.Configure<AuthorizationCacheOptions>(options => options.DefaultCacheDuration = cacheOptions?.DefaultCacheDuration ?? TimeSpan.FromMinutes(60));
        return services;
    }
}
