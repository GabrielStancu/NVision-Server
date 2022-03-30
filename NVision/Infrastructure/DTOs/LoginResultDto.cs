using Core.Models;

namespace Infrastructure.DTOs
{
    public class LoginResultDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public UserType UserType { get; set; }
        public string ProfilePictureSrc { get; set; }
        public string Token { get; set;  }
        public bool CompleteProfile { get; set; }
    }
}
