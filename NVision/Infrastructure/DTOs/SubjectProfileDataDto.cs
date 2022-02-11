using System;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class SubjectProfileDataDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string ProfilePictureSrc { get; set; }
        public int? WatcherId { get; set; }
        public string WatcherFullName { get; set; }
        public string Address { get; set; }
        public bool IsPatient { get; set; }
        public char Sex { get; set; }
        public IEnumerable<WatcherOptionDto> Watchers { get; set; }
            = new List<WatcherOptionDto>();
    }
}
