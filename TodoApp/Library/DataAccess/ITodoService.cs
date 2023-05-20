using Library.Models;

namespace Library.DataAccess
{
    public interface ITodoService
    {
        Task CompleteTodo(int id, int assignTo);
        Task<Todo?> CreateTodo(int assignTo, string task);
        Task DeleteTodo(int id, int assignTo);
        Task<Todo?> GetSingleTodo(Todo todo);
        Task<List<Todo>> GetTodos(Todo todo);
        Task UpdateTodo(int id, int assignTo, string task);
    }
}