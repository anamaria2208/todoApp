
using backend.Model;
using backend.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class TodoItemContext : DbContext
    {
        public TodoItemContext(DbContextOptions<TodoItemContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}