// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HamedStack.AspNetCore.Security.DynamicPermission;
/// <summary>
/// Provides a handler for custom permission authorization requirements.
/// </summary>
public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ILogger<PermissionRequirementHandler> _logger;

    /// <summary>
    /// Specifies the claim type for dynamic permissions.
    /// </summary>
    public const string ClaimType = "DynamicPermission";

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionRequirementHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public PermissionRequirementHandler(ILogger<PermissionRequirementHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Evaluates the authorization context to determine if it meets the permission requirement.
    /// </summary>
    /// <param name="context">The authorization context.</param>
    /// <param name="requirement">The permission requirement to evaluate.</param>
    /// <returns>An asynchronous task that represents the completion of the authorization check.</returns>
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context.User.Claims.Where(
            x => x.Type == ClaimType && x.Value == requirement.Permission);
        
        if (permissions.Any())
        {
            _logger.LogInformation($"Authorization succeeded for requirement {requirement.Permission}");
            context.Succeed(requirement);
            await Task.CompletedTask.ConfigureAwait(false);
        }
        else
        {
            _logger.LogWarning($"Authorization failed for requirement {requirement.Permission}");
        }
    }
}

