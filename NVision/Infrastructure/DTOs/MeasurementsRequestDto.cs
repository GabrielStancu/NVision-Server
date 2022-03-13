using Core.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class MeasurementsRequestDto
    {
        public int SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<SensorType> SensorTypes { get; set; }
    }
}
