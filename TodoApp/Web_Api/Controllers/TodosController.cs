using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.DataAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Web_Api.StartupConfig;
using System.Threading.Tasks;

namespace Web_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly IUtilityLogger<TodosController> _log;
    private readonly ITodoService _todo;

    public TodosController(ITodoService todo, IUtilityLogger<TodosController> log)
    {
        _todo = todo;
        _log = log;
    }

    // GET: api/Todos
    [HttpGet(Name = "GetAllTodos")]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        _log.Information("Get: Call to api/Todos:");

        Todo Obj = new()
        {
            AssignTo = GetUseId(),
        };

        try
        {
            var result = await _todo.GetTodos(Obj);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _log.Error("Call to api/Todos and Getting data failed.", ex);
            return BadRequest();
        }
    }

    // GET: api/Todos/5
    [HttpGet("{todoId}", Name = "GetTodo")]
    public async Task<ActionResult<Todo?>> Get(int todoId)
    {
        _log.Information($"Get: Call to api/Todos/{todoId}:");

        Todo Obj = new()
        {
            Id = todoId,
            AssignTo = GetUseId(),
        };

        try
        {
            var result = await _todo.GetSingleTodo(Obj);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _log.Error($"Call to api/Todos/Id and Getting data failed. the Id was {todoId}", ex);
            return BadRequest();
        }
    }

    // POST: api/Todos
    [HttpPost( Name = "CreateTodo")]
    public async Task<ActionResult<Todo>> Create([FromBody] string task)
    {
        _log.Information($"Post: Call to api/Todos/ (Task: {task}):");

        try
        {
            var result = await _todo.CreateTodo(GetUseId(), task);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _log.Error($"Call to api/Todos/ and Getting data failed. the Task was {task}", ex);
            return BadRequest();
        }
    }

    // PUT: api/Todos/5
    [HttpPut("{todoId}", Name = "UpdateTodoTask")]
    public void Update(int todoId, [FromBody] string task)
    {
        _log.Information($"Put: Call to api/Todos/ (Task: {task}):");

        try
        {
            _todo.UpdateTodo(todoId, GetUseId(), task);
        }
        catch (Exception ex)
        {
            _log.Error($"The Put Call to api/Todos/{todoId} failed. the Task was {task}", ex);
        }
    }

    // GET: api/Todos/5/Complete
    [HttpPut("{todoId}/Complete", Name = "UpdateTodoComplete")]
    public void Complete(int todoId)
    {
        _log.Information("Put: Call to api/Todos/todoId:");

        try
        {
            _todo.CompleteTodo(todoId, GetUseId());
        }
        catch (Exception ex)
        {
            _log.Error($"The Put Call to api/Todos/todoId/Complete failed. the Id was {todoId}", ex);
        }
    }


    // GET: api/Todos/5
    [HttpDelete("{todoId}", Name = "DeleteTodo")]
    public void Delete(int todoId)
    {
        _log.Information("Delete: Call to api/Todos/todoId:");

        try
        {
            _todo.DeleteTodo(todoId, GetUseId());
        }
        catch (Exception ex)
        {
            _log.Error($"The Delete Call to api/Todos/todoId failed. The Id was {todoId}", ex);
        }
    }

    private int GetUseId()
    {
        var actualId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return Convert.ToInt32(actualId);
    }
}
