using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.DB;
using Microsoft.EntityFrameworkCore;

namespace HelloToAsp.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        private readonly ToDoListContext _context;

        public UsersRepository(ToDoListContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetDetails(int id)
        {
            return await _context.Users.Include(q => q.ToDoLists)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
