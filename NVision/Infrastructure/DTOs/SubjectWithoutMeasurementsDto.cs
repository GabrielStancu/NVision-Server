using System;

namespace Infrastructure.DTOs
{
    public class SubjectWithoutMeasurementsDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int WatcherId { get; set; }
        public string Address { get; set; }
        public bool IsPatient { get; set; }
        public char Sex { get; set; }
        public string HealthStatus { get; set; }
    }
}
