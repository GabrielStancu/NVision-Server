using System;

namespace Infrastructure.DTOs
{
    public class AlertDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public bool? WasTrueAlert { get; set; }
    }
}
