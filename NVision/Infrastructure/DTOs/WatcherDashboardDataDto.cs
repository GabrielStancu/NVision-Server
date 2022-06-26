using System;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class WatcherDashboardDataDto
    {
        public IEnumerable<DashboardCardDataDto> Cards { get; set; }
        public IEnumerable<DashboardSubjectDataDto> Subjects { get; set; }
        public IEnumerable<DashboardAlertDataDto> Alerts { get; set; }
    }

    public class DashboardCardDataDto
    {
        public int NumericValue { get; set; }
        public string PropertyName { get; set; }
    }

    public class DashboardSubjectDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HealthStatus { get; set; }
        public string ProfilePictureSrc { get; set; }
        public int HealthScore { get; set; }
    }

    public class DashboardAlertDataDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool? WasTrueAlert { get; set; }
    }
}
