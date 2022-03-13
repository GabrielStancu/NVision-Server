using System;

namespace Infrastructure.DTOs
{
    public  class SubjectSummarizedDataDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string ProfilePictureSrc { get; set; }
        public bool IsPatient { get; set; }
        public string HealthStatus { get; set; }
    }
}
