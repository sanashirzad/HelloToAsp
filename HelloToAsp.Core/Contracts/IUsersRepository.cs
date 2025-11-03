using HelloToAsp.Data;

namespace HelloToAsp.Core.Contracts
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetDetails(int id);
    }
}
