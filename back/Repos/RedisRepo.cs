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