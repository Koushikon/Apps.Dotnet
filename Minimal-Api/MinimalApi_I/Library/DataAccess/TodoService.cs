using Library.Models;

namespace Library.DataAccess;

public class TodoService : ITodoService
{
    private readonly IDBAccessService _sql;

    public TodoService(IDBAccessService sql)
    {
        _sql = sql;
    }

    public Task<List<Todo>> GetTodos(Todo todo)
    {
        const string spName = "ManageTodo";

        return _sql.LoadData<Todo, dynamic>(spName, new { todo.Id, todo.AssignTo, todo.IsComplete }, "Default");
    }

    public async Task<Todo?> GetSingleTodo(Todo todo)
    {
        const string spName = "ManageTodo";

        var results = await _sql.LoadData<Todo, dynamic>(spName, new { todo.Id, todo.AssignTo, todo.IsComplete }, "Default");

        return results.FirstOrDefault();
    }

    public async Task<Todo?> CreateTodo(int assignTo, string task)
    {
        const string spName = "AddTodo";

        var results = await _sql.LoadData<Todo, dynamic>(spName, new { AssignTo = assignTo, Task = task }, "Default");

        return results.SingleOrDefault();
    }

    public Task UpdateTodo(int id, int assignTo, string task)
    {
        const string spName = "UpdateTodo";

        return _sql.SaveData<dynamic>(spName, new { Id = id, AssignTo = assignTo, Task = task }, "Default");
    }

    public Task CompleteTodo(int id, int assignTo)
    {
        const string spName = "CompleteTodo";

        return _sql.SaveData<dynamic>(spName, new { Id = id, AssignTo = assignTo }, "Default");
    }

    public Task DeleteTodo(int id, int assignTo)
    {
        const string spName = "DeleteTodo";

        return _sql.SaveData<dynamic>(spName, new { Id = id, AssignTo = assignTo }, "Default");
    }
}
