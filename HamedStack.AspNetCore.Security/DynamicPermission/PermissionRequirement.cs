// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;

namespace HamedStack.AspNetCore.Security.DynamicPermission;

/// <summary>
/// Represents a custom permission authorization requirement.
/// </summary>
public class PermissionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionRequirement"/> class.
    /// </summary>
    /// <param name="permission">The permission string.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="permission"/> is null or empty.</exception>
    public PermissionRequirement(string permission)
    {
        Permission = !string.IsNullOrEmpty(permission) ? permission : throw new ArgumentNullException(nameof(permission));
    }

    /// <summary>
    /// Gets the permission string associated with this requirement.
    /// </summary>
    public string Permission { get; }
}