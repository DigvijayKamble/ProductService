namespace ProductService.Infrastructure.Configuration;
public static class Constants
{
    public const string DatabaseConnectionString = "Database:ConnectionString";
    public const string EnableDevMode = "Database:EnableDevMode";
    public const string Hazelcast = "Hazelcast";
    public const string CacheUniqueId = "Hazelcast:CacheUniqueIdentifier";
    public const string DefaultProxyHttpClient = "DefaultProxyHttpClient";
}

public class AppSettings
{
    public required JwtSettings JwtSettings { get; init; }
    public required ProxySettings ProxySettings { get; init; }
    public required SmtpSettings SmtpSettings { get; init; }
}

public class JwtSettings
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int TokenExpirationInMinutes { get; init; } 
    public int RefreshTokenExpirationDays { get; init; }
    public string PrivateKeyPath { get; init; } = string.Empty;
    public string PublicKeyPath { get; init; } = string.Empty;
}

public sealed class ProxySettings
{
    public string Url { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public sealed class SmtpSettings
{
    public string FromEmail { get; init; } = string.Empty;
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

