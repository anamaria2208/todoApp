using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Model;
using backend.Model.DTOs;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userResponse)
        {
            if (userResponse == null){
                return BadRequest("Invalid user data");
            }
            else {
                var user = new User
                {
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userResponse.Password),
                    Username = userResponse.Username
                };
                await _userRepository.AddUserAsync(user);
                return Ok();
            }
        }
    }
}