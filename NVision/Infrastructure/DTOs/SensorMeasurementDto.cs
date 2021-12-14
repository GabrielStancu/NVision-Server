using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class SensorMeasurementDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public SensorType SensorType { get; set; }
        public string SensorName { get; set; }
    }
}
