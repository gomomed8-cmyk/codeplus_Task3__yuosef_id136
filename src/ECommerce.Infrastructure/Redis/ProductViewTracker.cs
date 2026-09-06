using ECommerce.Application.Interfaces.Services;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Redis;

public sealed class ProductViewTracker : IProductViewTracker
{
    private readonly IDatabase _database;

    public ProductViewTracker(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task IncrementAsync(int productId)
    {
        var key = $"product:views:{productId}";

        await _database.StringIncrementAsync(key);
    }

    public async Task<int> GetViewCountAsync(int productId)
    {
        var key = $"product:views:{productId}";

        var value = await _database.StringGetAsync(key);

        return value.HasValue ? (int)value : 0;
    }

    public async Task<long> GetAndResetAsync(int productId)
    {
        var key = $"product:views:{productId}";

        var result = await _database.ScriptEvaluateAsync(
            """
            local value = redis.call('GET', KEYS[1])

            if value then
                redis.call('SET', KEYS[1], 0)
                return tonumber(value)
            end

            return 0
            """,
            new RedisKey[] { key });

        return (long)result;
    }
}