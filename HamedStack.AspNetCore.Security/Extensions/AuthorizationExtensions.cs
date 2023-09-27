// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;

namespace HamedStack.AspNetCore.Security.Extensions;

/// <summary>
/// Provides extension methods to simplify adding authorization policies based on roles, claims, and custom requirements.
/// </summary>
public static class AuthorizationExtensions
{
    /// <summary>
    /// Adds an authorization policy that requires specified roles.
    /// </summary>
    /// <param name="options">The authorization options to which the policy is added.</param>
    /// <param name="policyName">The name of the policy.</param>
    /// <param name="requiredRoles">An array of roles that are required for the policy.</param>
    /// <returns>The updated authorization options.</returns>
    /// <remarks>
    /// This method simplifies the process of adding role-based policies to the provided authorization options.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.AddAuthorization(options =>
    /// {
    ///     options.AddRolesPolicy("AdminPolicy", "Admin");
    /// });
    /// </code>
    /// </example>
    public static AuthorizationOptions AddRolesPolicy(this AuthorizationOptions options, string policyName, params string[] requiredRoles)
    {
        options.AddPolicy(policyName, policy => policy.RequireRole(requiredRoles));
        return options;
    }

    /// <summary>
    /// Adds an authorization policy that requires a specific claim with one of the specified values.
    /// </summary>
    /// <param name="options">The authorization options to which the policy is added.</param>
    /// <param name="policyName">The name of the policy.</param>
    /// <param name="claimType">The type of the claim.</param>
    /// <param name="requiredValues">An array of required values for the claim.</param>
    /// <returns>The updated authorization options.</returns>
    /// <remarks>
    /// This method simplifies the process of adding claim-based policies to the provided authorization options.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.AddAuthorization(options =>
    /// {
    ///     options.AddClaimsPolicy("ReadAccessPolicy", "access", "read");
    /// });
    /// </code>
    /// </example>
    public static AuthorizationOptions AddClaimsPolicy(this AuthorizationOptions options, string policyName, string claimType, params string[] requiredValues)
    {
        options.AddPolicy(policyName, policy => policy.RequireClaim(claimType, requiredValues));
        return options;
    }

    /// <summary>
    /// Adds a custom authorization policy with a specified requirement.
    /// </summary>
    /// <typeparam name="TRequirement">The type of the requirement. This type should implement <see cref="IAuthorizationRequirement"/>.</typeparam>
    /// <param name="options">The authorization options to which the policy is added.</param>
    /// <param name="policyName">The name of the policy.</param>
    /// <param name="requirement">The requirement instance.</param>
    /// <returns>The updated authorization options.</returns>
    /// <remarks>
    /// This method offers a flexible way to add custom requirements to the provided authorization options.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.AddAuthorization(options =>
    /// {
    ///     var customRequirement = new MyCustomRequirement();
    ///     options.AddCustomPolicy("CustomPolicy", customRequirement);
    /// });
    /// </code>
    /// </example>
    public static AuthorizationOptions AddCustomPolicy<TRequirement>(this AuthorizationOptions options, string policyName, TRequirement requirement) where TRequirement : IAuthorizationRequirement, new()
    {
        options.AddPolicy(policyName, policy => policy.Requirements.Add(requirement));
        return options;
    }
}
