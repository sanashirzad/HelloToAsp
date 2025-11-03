using AutoMapper;
using HelloToAsp.Core.Contracts;
using HelloToAsp.Data;
using HelloToAsp.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace HelloToAsp.Core.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        private readonly ToDoListContext _context;

        public UsersRepository(ToDoListContext context, IMapper mapper) : base(context, mapper)
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
