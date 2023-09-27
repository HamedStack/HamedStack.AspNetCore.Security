namespace HamedStack.AspNetCore.Security.DynamicPermission;

/// <summary>
/// Holds the configuration settings for caching.
/// </summary>
public class AuthorizationCacheOptions
{
    /// <summary>
    /// Gets or sets the default cache duration.
    /// </summary>
    public TimeSpan DefaultCacheDuration { get; set; }
}