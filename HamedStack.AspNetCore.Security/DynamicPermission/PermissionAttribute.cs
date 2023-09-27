// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;

namespace HamedStack.AspNetCore.Security.DynamicPermission;

/// <summary>
/// Represents an attribute that specifies authorization based on custom permissions.
/// </summary>
/// <remarks>
/// This attribute can be applied to both classes (e.g., controllers) and methods (e.g., action methods).
/// It extends the base <see cref="AuthorizeAttribute"/> and adds a description property which provides a more detailed context for the permission.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class PermissionAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionAttribute"/> class.
    /// </summary>
    public PermissionAttribute() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionAttribute"/> class with the specified policy.
    /// </summary>
    /// <param name="policy">The name of the policy to use for authorization.</param>
    public PermissionAttribute(string policy) : base(policy) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionAttribute"/> class with the specified policy and description.
    /// </summary>
    /// <param name="policy">The name of the policy to use for authorization.</param>
    /// <param name="description">A brief description of the permission.</param>
    public PermissionAttribute(string policy, string? description) : base(policy)
    {
        Description = description;
    }

    /// <summary>
    /// Gets or sets a description for the permission.
    /// </summary>
    /// <value>The description of the permission, or null if no description has been set.</value>
    public string? Description { get; set; }
}
