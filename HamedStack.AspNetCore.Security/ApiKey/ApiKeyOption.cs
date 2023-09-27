namespace HamedStack.AspNetCore.Security.ApiKey;

/// <summary>
/// Configuration options for the ApiKeyMiddleware.
/// </summary>
public class ApiKeyOption
{
    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public string ApiKey { get; set; } = null!;

    /// <summary>
    /// Gets or sets the expiry of the API key in days.
    /// If set to null, the API key will not expire.
    /// </summary>
    public int? ExpiryInDays { get; set; }
}