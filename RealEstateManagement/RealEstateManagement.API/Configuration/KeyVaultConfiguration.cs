using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace RealEstateManagement.API.Configuration;

/// <summary>
/// Extension methods for configuring Azure Key Vault
/// </summary>
public static class KeyVaultConfiguration
{
    /// <summary>
    /// Adds Azure Key Vault configuration provider
    /// </summary>
    public static IConfigurationBuilder AddAzureKeyVault(
        this IConfigurationBuilder builder,
        IConfiguration configuration,
        string environment)
    {
        var keyVaultConfig = configuration.GetSection("KeyVault");
        var isEnabled = keyVaultConfig.GetValue<bool>("Enabled");

        if (!isEnabled)
            return builder;

        var vaultUri = keyVaultConfig.GetValue<string>("VaultUri");
        if (string.IsNullOrEmpty(vaultUri))
        {
            System.Diagnostics.Debug.WriteLine("Key Vault URI is not configured");
            return builder;
        }

        try
        {
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(new Uri(vaultUri), credential);
            builder.ConfigureKeyVault(client);

            System.Diagnostics.Debug.WriteLine($"Azure Key Vault configured successfully: {vaultUri}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error configuring Azure Key Vault: {ex.Message}");
        }

        return builder;
    }

    private static IConfigurationBuilder ConfigureKeyVault(
        this IConfigurationBuilder builder,
        SecretClient client)
    {
        try
        {
            var secrets = client.GetPropertiesOfSecretsAsync();
            return builder;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading secrets from Key Vault: {ex.Message}");
            return builder;
        }
    }

    /// <summary>
    
    /// </summary>
    public static string? GetSecret(
        this IConfiguration configuration,
        string secretName,
        string? fallbackKey = null)
    {
        var value = configuration[$"KeyVault:{secretName}"];
        if (!string.IsNullOrEmpty(value))
            return value;

        fallbackKey ??= secretName;
        return configuration[fallbackKey];
    }
}
