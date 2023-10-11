using System.IdentityModel.Tokens.Jwt;
using System.Text;
using backend.Model;
using backend.Model.DTOs;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDto userRequest)
        {
            // missing email or password
            if (string.IsNullOrEmpty(userRequest.Username) || string.IsNullOrEmpty(userRequest.Password)){
                return BadRequest("ERROR: Invalid user data");
            }

            // check if username alredy exist in database
            if (await CheckIfUsernameExist(userRequest.Username)){
                return Conflict("ERROR: Username already exist");
            }

            // hash password and create new user
            var hashPassword = HashPassword(userRequest.Password);
            await CreateUser(userRequest.Username, hashPassword);
            return Ok("SUCESS: New user created");
            
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserDto userRequest)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userRequest.Username);
            var verifiedPassword = false;

            if (user != null){
                verifiedPassword = VerifyPassword(userRequest.Password, user.PasswordHash);
            }
            if (user == null || !verifiedPassword){
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            // todo : issue jwt token
            var jwtToken = GenerateJwtToken(userRequest.Username);
             
            return Ok(jwtToken);
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

        private string GenerateJwtToken(string username)
        {
            // symetric security key (create and verify jwt token)
            var secretKey = _configuration.GetSection("JwtSettings")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.UtcNow.AddHours(12), // Token expiration time
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}