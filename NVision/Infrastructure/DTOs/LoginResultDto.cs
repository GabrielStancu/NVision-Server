using Core.Models;

namespace Infrastructure.DTOs
{
    public class LoginResultDto
    {
        public string Username { get; set; }
        public UserType UserType { get; set; }
    }
}
