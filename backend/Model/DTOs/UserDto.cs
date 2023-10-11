namespace backend.Model.DTOs
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; } // plain text password
    }
}