using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoSession.DataAccess;
using TodoSession.DataAccess.Models;
using TodoSession.BLL.Services.Interfaces;
using TodoSession.BLL.DTO.Todo;

namespace TodoSession.WebApi.Controllers
{
    [Route("items")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                Response.StatusCode = 403;
                return new JsonResult(new { error = "forbidden" });
            }

            var todos = await _todoService.GetTodosAsync(userId);
            return Ok(new { items = todos });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto dto)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var newTodo = await _todoService.CreateTodoAsync(dto, userId);
            return Ok(new { id = newTodo.Id });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromBody] UpdateTodoDto dto)
        {
            var updatedTodo = await _todoService.UpdateTodoAsync(dto);
            if (updatedTodo == null) return NotFound("Task Not Found");
            return Ok(updatedTodo);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodo([FromBody] DeleteTodoDto dto)
        {
            var deletedTodo = await _todoService.DeleteTodoAsync(dto.Id);
            if (deletedTodo == null) return NotFound("Task Not Found");
            return Ok(new { ok = true });
        }
    }
}
