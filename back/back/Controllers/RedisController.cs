using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public RedisController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCacheValue([FromBody] string key)
        {
            var value = await _redisService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? (IActionResult)NotFound() : Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> SetCacheValue([FromBody]CacheItemDTO cacheItem)
        {
            await _redisService.SetCacheValueAsync(cacheItem.Key, cacheItem.Value);
            return Ok();
        }


    }
}
