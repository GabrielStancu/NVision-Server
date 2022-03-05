using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class SensorReadingDto
    {
        public string Type { get; set; }
        public double Value { get; set; }
        public string Timestamp { get; set; }
        public Guid DeviceSerial { get; set; }
    }
}
