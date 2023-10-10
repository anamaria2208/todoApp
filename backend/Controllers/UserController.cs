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

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDto userResponse)
        {
            // missing email or password
            if (string.IsNullOrEmpty(userResponse.Username) || string.IsNullOrEmpty(userResponse.Password)){
                return BadRequest("Invalid user data");
            }

            // check if username alredy exist in database
            if (await CheckIfUsernameExist(userResponse.Username)){
                return Conflict("Username already exist");
            }

            // hash password and create new user
            else {
                var hashPassword = HashPassword(userResponse.Password);
                await CreateUser(userResponse.Username, hashPassword);
                return Ok("New user created");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserDto userResponse)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userResponse.Username);
            var verifiedPassword = false;

            if (user != null){
                verifiedPassword = VerifyPassword(userResponse.Password, user.PasswordHash);
            }
            if (user == null || !verifiedPassword){
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            // todo : issue jwt token
             
            return Ok();
        }

        private async Task<bool> CheckIfUsernameExist(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username) != null;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string requestPassword, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(requestPassword, dbPassword);
        }

        private async Task CreateUser(string username, string hashedPassword)
        {
            var user = new User 
            {
                Username = username,
                PasswordHash = hashedPassword
            };
            await _userRepository.AddUserAsync(user);
        }
    }
}