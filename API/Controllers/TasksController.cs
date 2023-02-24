using API.DTOs;
using API.Exceptions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;
    private readonly ITasksService _tasksService;

    public TasksController(ILogger<TasksController> logger, ITasksService tasksService)
    {
        _logger = logger;
        _tasksService = tasksService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        try
        {
            var tasks = await _tasksService.GetTasksAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException || ex is TaskEntityNotFoundException)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTasks(Guid id)
    {
        try
        {
            var task = await _tasksService.GetTaskAsync(id);
            return Ok(task);
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException || ex is TaskEntityNotFoundException)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(TaskDTO task)
    {
        try
        {
            var updatedTask = await _tasksService.UpdateAsync(task);
            return Ok(task);
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException || ex is TaskEntityNotFoundException)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        try
        {
            await _tasksService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException || ex is TaskEntityNotFoundException)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskDTO task)
    {
        try
        {
            var createdTask = await _tasksService.CreateAsync(task);
            return Ok(createdTask);
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException || ex is TaskEntityAlreadyExistException)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("status-list")]
    public async Task<IActionResult> GetStatusList()
    {
        try
        {
            return Ok(await _tasksService.GetStatusListAsync());
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
}
