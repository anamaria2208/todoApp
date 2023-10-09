using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Model;
using backend.Model.DTOs;
using backend.Repository.Interfaces;

namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoItemContext _context;
        public UserRepository(TodoItemContext context)
        {
            _context = context;
        }

        // create new user
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }


    }
}