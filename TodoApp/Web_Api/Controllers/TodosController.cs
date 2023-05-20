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
    private readonly IConfiguration _config;
    private readonly ILogger<TodosController> _log;
    private readonly ITodoService _todo;

    public TodosController(ITodoService todo, IConfiguration config, ILogger<TodosController> log)
    {
        _todo = todo;
        _config = config;
        _log = log;
    }

    // GET: api/Todos
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        _log.LogInformation("Get: Call to api/Todos:");

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
            _log.LogError(ex, "Call to api/Todos and Getting data failed.");
            return BadRequest();
        }
    }

    // GET: api/Todos/5
    [HttpGet("{todoId}")]
    public async Task<ActionResult<Todo?>> Get(int todoId)
    {
        _log.LogInformation("Get: Call to api/Todos/{todoId}:", todoId);

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
            _log.LogError(ex, "Call to {ApiPath} and Getting data failed. the Id was {todoId}", $"api/Todos/Id", todoId);
            return BadRequest();
        }
    }

    // POST: api/Todos
    [HttpPost]
    public async Task<ActionResult<Todo>> Create([FromBody] string task)
    {
        _log.LogInformation("Post: Call to api/Todos/ (Task: {task}):", task);

        try
        {
            var result = await _todo.CreateTodo(GetUseId(), task);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Call to {ApiPath} and Getting data failed. the Task was {task}", task);
            return BadRequest();
        }
    }

    // PUT: api/Todos/5
    [HttpPut("{todoId}")]
    public void Update(int todoId, [FromBody] string task)
    {
        _log.LogInformation("Put: Call to api/Todos/ (Task: {task}):", task);

        
        try
        {
            _todo.UpdateTodo(todoId, GetUseId(), task);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The Put Call to api/Todos/{todoId} failed. the Task was {task}", todoId, task);
        }
    }

    // GET: api/Todos/5/Complete
    [HttpPut("{todoId}/Complete")]
    public void Complete(int todoId)
    {
        _log.LogInformation("Put: Call to api/Todos/todoId:");

        try
        {
            _todo.CompleteTodo(todoId, GetUseId());
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The Put Call to api/Todos/todoId/Complete failed. the Id was {Id}", todoId);
        }
    }


    // GET: api/Todos/5
    [HttpDelete("{todoId}")]
    public void Delete(int todoId)
    {
        _log.LogInformation("Delete: Call to api/Todos/todoId:");

        try
        {
            _todo.DeleteTodo(todoId, GetUseId());
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The Delete Call to api/Todos/todoId  failed. the Id was {Id}", todoId);
        }
    }

    private int GetUseId()
    {
        var actualId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return Convert.ToInt32(actualId);
    }
}
