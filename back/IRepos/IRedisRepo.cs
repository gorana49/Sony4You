namespace back.IRepos
{
    public interface IRedisRepo 
    {
        Task<string> GetTime();
    }
}