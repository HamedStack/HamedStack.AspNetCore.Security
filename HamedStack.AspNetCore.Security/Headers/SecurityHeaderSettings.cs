// ReSharper disable StringLiteralTypo
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace HamedStack.AspNetCore.Security.Headers;

/// <summary>
/// Represents the security header settings for configuring response headers.
/// </summary>
public class SecurityHeaderSettings
{
    /// <summary>
    /// Gets or sets the value for the X-Frame-Options header.
    /// </summary>
    public string XFrameOptions { get; set; } = "DENY";

    /// <summary>
    /// Gets or sets the value for the X-Permitted-Cross-Domain-Policies header.
    /// </summary>
    public string XPermittedCrossDomainPolicies { get; set; } = "none";

    /// <summary>
    /// Gets or sets the value for the X-Xss-Protection header.
    /// </summary>
    public string XXssProtection { get; set; } = "1; mode=block";

    /// <summary>
    /// Gets or sets the value for the X-Content-Type-Options header.
    /// </summary>
    public string XContentTypeOptions { get; set; } = "nosniff";

    /// <summary>
    /// Gets or sets the value for the Referrer-Policy header.
    /// </summary>
    public string ReferrerPolicy { get; set; } = "no-referrer";

    /// <summary>
    /// Gets or sets the value for the Permissions-Policy header.
    /// </summary>
    public string PermissionsPolicy { get; set; } = "camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), usb=()";

    /// <summary>
    /// Gets or sets the value for the Content-Security-Policy header.
    /// </summary>
    public string ContentSecurityPolicy { get; set; } = "default-src 'self'";

    /// <summary>
    /// Gets or sets the custom headers.
    /// </summary>
    public Dictionary<string, string> CustomHeaders { get; set; } = new();
}