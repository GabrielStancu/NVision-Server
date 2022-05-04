using System;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class DeviceAlertDto
    {
        public string DeviceSerialNumber { get; set; }
        public DateTime AlertMoment { get; set; }
        public string[] Parameters { get; set; }
    }
}
