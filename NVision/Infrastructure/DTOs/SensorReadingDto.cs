using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class SensorReadingDto
    {
        public Guid DeviceSerialNumber { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public SensorType SensorType { get; set; }
    }
}
