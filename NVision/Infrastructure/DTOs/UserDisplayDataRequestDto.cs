using Core.Models;

namespace Infrastructure.DTOs
{
    public class UserDisplayDataRequestDto
    {
        public int UserId { get; set; }
        public UserType UserType { get; set; } 
    }
}
