using StackExchange.Redis;

namespace RealEstateManagement.API.Middleware;

/// <summary>
/// Caching Service for distributed cache operations
/// </summary>
public interface ICachingService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
    Task<bool> ExistsAsync(string key);
}

public class RedisCachingService : ICachingService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;
    private readonly ILogger<RedisCachingService> _logger;

    public RedisCachingService(IConnectionMultiplexer connectionMultiplexer, ILogger<RedisCachingService> logger)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _database = _connectionMultiplexer.GetDatabase();
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            var value = await _database.StringGetAsync(key);
            if (!value.IsNull)
            {
                var deserialized = System.Text.Json.JsonSerializer.Deserialize<T>(value.ToString());
                _logger.LogInformation("Cache hit for key: {CacheKey}", key);
                return deserialized;
            }
            _logger.LogInformation("Cache miss for key: {CacheKey}", key);
            return default;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving from cache for key: {CacheKey}", key);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        try
        {
            var serialized = System.Text.Json.JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serialized, expiration);
            _logger.LogInformation("Cache set for key: {CacheKey}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting cache for key: {CacheKey}", key);
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await _database.KeyDeleteAsync(key);
            _logger.LogInformation("Cache removed for key: {CacheKey}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing cache for key: {CacheKey}", key);
        }
    }

    public async Task<bool> ExistsAsync(string key)
    {
        try
        {
            return await _database.KeyExistsAsync(key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking cache existence for key: {CacheKey}", key);
            return false;
        }
    }
}
