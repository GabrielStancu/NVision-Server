using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class User : Model
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string ProfilePictureSrc { get; set; }
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
    }
}
