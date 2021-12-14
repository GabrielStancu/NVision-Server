using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class WatcherHomepageDataDto
    {
        public IEnumerable<SubjectWithoutMeasurementsDto> Subjects { get; set; }
        public int SubjectsCount { get; set; }
        public IEnumerable<AlertDto> Alerts { get; set; } 
    }
}
