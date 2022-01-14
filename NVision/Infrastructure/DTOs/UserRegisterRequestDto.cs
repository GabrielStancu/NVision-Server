using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class UserRegisterRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public UserType UserType { get; set; }
    }
}
