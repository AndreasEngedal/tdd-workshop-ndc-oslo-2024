using ClientRiskEvaluator;
using Microsoft.Extensions.Caching.Distributed;

public class RedisBlockList : IBlockList
{
    private readonly IDistributedCache _cache;

    public RedisBlockList(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<bool> IsBlocked(string email)
    {
        var client = await _cache.GetStringAsync(email);

        return !string.IsNullOrEmpty(client);
    }
}