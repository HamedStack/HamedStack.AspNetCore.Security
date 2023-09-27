// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HamedStack.AspNetCore.Security.DynamicPermission;

/// <summary>
/// Represents a provider for authorization policies with the capability to fallback to custom permission requirements.
/// </summary>
/// <remarks>
/// This provider tries to retrieve the policy using the default behavior. If no policy is found, it will create a new policy based on a <see cref="PermissionRequirement"/> with the given policy name.
/// </remarks>
public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly IMemoryCache _cache;
    private readonly AuthorizationCacheOptions _cacheOptions;
    private readonly ILogger<AuthorizationPolicyProvider> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationPolicyProvider"/> class.
    /// </summary>
    /// <param name="options">The options for the <see cref="AuthorizationOptions"/>.</param>
    /// <param name="cache">The memory cache.</param>
    /// <param name="cacheOptions">The cache options.</param>
    /// <param name="logger">The logger.</param>
    public AuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options,
        IMemoryCache cache,
        IOptions<AuthorizationCacheOptions> cacheOptions,
        ILogger<AuthorizationPolicyProvider> logger) : base(options)
    {
        _cache = cache;
        _cacheOptions = cacheOptions.Value;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves an authorization policy by name, or creates a new one based on a permission requirement if not found.
    /// </summary>
    /// <param name="policyName">The name of the policy to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="AuthorizationPolicy"/> associated with the provided policy name, or a new policy with a permission requirement if not found.
    /// </returns>
    /// <remarks>
    /// If no policy is found using the default provider logic, a new policy is constructed with a <see cref="PermissionRequirement"/> using the given policy name.
    /// </remarks>
    /// <example>
    /// <code>
    /// var policy = await authorizationPolicyProvider.GetPolicyAsync("CustomPolicyName");
    /// </code>
    /// </example>
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        return await _cache.GetOrCreateAsync(policyName, async entry =>
        {
            entry.SetAbsoluteExpiration(_cacheOptions.DefaultCacheDuration);

            var policy = await base.GetPolicyAsync(policyName);

            if (policy != null) return policy;

            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();
            _logger.LogInformation($"Generated policy {policyName}");

            return policy;
        }).ConfigureAwait(false);
    }
}