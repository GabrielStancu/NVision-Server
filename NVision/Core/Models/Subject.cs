using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Subject : User
    {
        public int WatcherId { get; set; }
        [ForeignKey("WatcherId")]
        [InverseProperty("Subjects")]
        public Watcher Watcher { get; set; }
        public new UserType UserType { get; } = UserType.Subject;
        public string Address { get; set; }
        [InverseProperty("Subject")]
        public IEnumerable<SensorMeasurement> SensorMeasurements { get; set; }
        public bool IsPatient { get; set; }
        public char Sex { get; set; }
        public string HealthStatus { get; set; }
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
    }
}
