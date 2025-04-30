using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.DB;

namespace HelloToAsp.Repositories
{
    public class ToDoListRepository : GenericRepository<ToDoList>, IToDoListRepository
    {
        public ToDoListRepository(ToDoListContext context) : base(context)
        {
        }
    }
}
