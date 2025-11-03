using HelloToAsp.Data;
using HelloToAsp.Core.Dtos.ToDoList;

namespace HelloToAsp.Core.Contracts
{
    public interface IToDoListRepository
    {
        Task<ToDoList> GetAsync(int? id);
        Task<ToDoList> GetAsync(int userId, int? id);
        Task<List<ToDoListDto>> GetAllAsync(int userId);
        Task<ToDoList> AddAsync(ToDoList entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(ToDoList entity);
        Task<bool> Exists(int id);
    }
}
