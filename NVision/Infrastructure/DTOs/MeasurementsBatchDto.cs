using System;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class MeasurementsBatchDto
    {
        public IEnumerable<SensorReadingDto> Records { get; set; }
        public Guid DeviceSerial { get; set; }
    }
}
