using System;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class DeviceAlertDto
    {
        public Guid DeviceSerialNumber { get; set; }
        public DateTime AlertMoment { get; set; }
        public IEnumerable<ParameterDto> Parameters { get; set; }
    }
}
