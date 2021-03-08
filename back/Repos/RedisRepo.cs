using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using back.IRepos;
namespace back.Repos
{
    public class RedisRepo : IRedisRepo 
    {
        private readonly IDistributedCache _distributedCache;
        public RedisRepo(IDistributedCache distributedCache)
        {
           _distributedCache= distributedCache;
        }

        public async Task<string> GetTime()
		{
			var cacheKey = "TheTime";
			var existingTime = _distributedCache.GetString(cacheKey);
			if (!string.IsNullOrEmpty(existingTime))
			{
				return "Fetched from cache : " + existingTime;
			}
			else
			{
				existingTime = DateTime.UtcNow.ToString();
				_distributedCache.SetString(cacheKey, existingTime);
				return "Added to cache : " + existingTime;
			}
		}
    }
}