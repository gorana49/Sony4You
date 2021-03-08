using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Neo4j.Driver;
namespace back.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class ReneterController : ControllerBase
    {
        private readonly ILogger<ReneterController> _logger;
		private readonly IDistributedCache _distributedCache;
		private readonly IDriver _driverNeo4J;
		private readonly IRedis
 
        public ReneterController(ILogger<ReneterController> logger,IDistributedCache distributedCache, IDriver driverNeo4J)
        {
            _logger = logger;
            _distributedCache= distributedCache;
			_driverNeo4J = driverNeo4J;
        }


		[HttpGet]
		[Route("api/Renter/GetTime")]
		public async Task<string> GetTime()
		{
			return "something";
			
		}

		// [HttpGet]
		// [Route("api/Renter/GetTime")]
		// public async Task<string> GetTime()
		// {
		// 	var cacheKey = "TheTime";
		// 	var existingTime = _distributedCache.GetString(cacheKey);
		// 	if (!string.IsNullOrEmpty(existingTime))
		// 	{
		// 		return "Fetched from cache : " + existingTime;
		// 	}
		// 	else
		// 	{
		// 		existingTime = DateTime.UtcNow.ToString();
		// 		_distributedCache.SetString(cacheKey, existingTime);
		// 		return "Added to cache : " + existingTime;
		// 	}
		// }

		// [HttpGet]
		// [Route("api/Renter/SetTime")]
		// public async Task<string> SetTime()
		// {
		// 	var cacheKey = "TheTime";
		// 	//var existingTime = _distributedCache.GetString(cacheKey);
		// 	var existingTime = DateTime.UtcNow.ToString();
	    // 	_distributedCache.SetString(cacheKey, existingTime);
		// 	return "Added to cache : " + existingTime;

		// }
}
}
