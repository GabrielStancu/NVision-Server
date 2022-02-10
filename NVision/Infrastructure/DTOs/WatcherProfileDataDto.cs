using System;

namespace Infrastructure.DTOs
{
    public class WatcherProfileDataDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureSrc { get; set; }
    }
}
