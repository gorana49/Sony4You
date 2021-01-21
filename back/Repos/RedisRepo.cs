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
    }
}