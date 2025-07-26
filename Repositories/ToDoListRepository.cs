using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.DB;
using HelloToAsp.Dtos.ToDoList;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelloToAsp.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly IGenericRepository<ToDoList> _genericRepository;
        private readonly ToDoListContext _context;
        public ToDoListRepository(IGenericRepository<ToDoList> genericRepository,
            UserManager<User> userManager,
            ToDoListContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public async Task<ToDoList> AddAsync(ToDoList entity) => await _genericRepository.AddAsync(entity);

        public async Task DeleteAsync(int id) => await _genericRepository.DeleteAsync(id);

        public async Task<bool> Exists(int id) => await _genericRepository.Exists(id);

        public async Task<List<ToDoListDto>> GetAllAsync(int userId)
        {
            return await _context.ToDoLists.Include(t => t.User)
                .Where(t => t.UserId == userId)
                .Select(t => new ToDoListDto
                {
                    Id = t.Id,
                    IsCompleted = t.IsCompleted,
                    Task = t.Task
                })
                .ToListAsync();
        }

        public async Task<ToDoList> GetAsync(int? id) => await _genericRepository.GetAsync(id);

        public async Task<ToDoList> GetAsync(int userId, int? id)
        {
            return await _context.ToDoLists.Include(t => t.User)
                .Where(t => t.UserId == userId && t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ToDoList entity) => await _genericRepository.UpdateAsync(entity);
    }
}
