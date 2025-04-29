using HelloToAsp.Data;

namespace HelloToAsp.Contracts
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetDetails(int id);
    }
}
