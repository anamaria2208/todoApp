using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoItemContext _context;
        public TodoItemRepository(TodoItemContext context)
        {
            _context = context;
        }

        // get all to do items
        public async Task<IEnumerable<TodoItem>> GetAllTodoAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // get todo item by id
        public async Task<TodoItem> GetTodoByIdAsync(int id)
        {
           var task = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                return task;
            }
            else
            {
                throw new Exception("Todo item not found");
            }
        }
        
        // create new todo item
        public async Task AddTodoAsync(TodoItem todo)
        {
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();
        }

        // delete todo item by id
        public async Task DeleteTodoAsync(int id)
        {
            var todo = await GetTodoByIdAsync(id);
            if (todo != null){
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        // update todo item
        public async Task UpdateTodoAsync(int id, TodoItem todo)
        {
            var todoItem = await GetTodoByIdAsync(id);
            if (todoItem != null){
                todoItem.Name = todo.Name;
                todoItem.IsComplete = todo.IsComplete;
                await _context.SaveChangesAsync();
            }
        }
    }
}