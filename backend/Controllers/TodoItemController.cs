using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Model;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public TodoItemController(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            var todos = await _todoItemRepository.GetAllTodoAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodo(int id)
        {
            var todo = await _todoItemRepository.GetTodoByIdAsync(id);
            if (todo == null){
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] TodoItem todo)
        {
            if (todo == null){
                return BadRequest("Invalid todo item data");
            }
            else {
                await _todoItemRepository.AddTodoAsync(todo);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var todo = await _todoItemRepository.GetTodoByIdAsync(id);
            if (todo == null){
                return NotFound();
            }
            else {
                await _todoItemRepository.DeleteTodoAsync(id);
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, TodoItem todo)
        {
            var todoExist = await _todoItemRepository.GetTodoByIdAsync(id);
            if (todoExist == null){
                return NotFound();
            }
            await _todoItemRepository.UpdateTodoAsync(id, todo);
            return NoContent();
        }
    }
}