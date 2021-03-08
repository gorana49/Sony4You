public class RenterService : IRenterService
{
    private readonly IRedisRepo _redisRepo;
    private readonly INeo4jRepo _neo4jRepo;

    public FinancialsService(IRedisRepo redisRepo,
                                Ineo4jRepo neo4jrepo)
    {
        _redisRepo = redisRepo;
        _neo4jRepo = neo4jrepo;
    }

    public async Task<string> GetTime()
    {
        return await _redisRepo.GetTime();
    }