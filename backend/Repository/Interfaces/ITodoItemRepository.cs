using backend.Model;

namespace backend.Repository
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetAllTodoAsync ();
        Task<TodoItem> GetTodoByIdAsync (int id);
        Task AddTodoAsync (TodoItem todo);
        Task UpdateTodoAsync (int id, TodoItem todo);
        Task DeleteTodoAsync (int id);
    }
}