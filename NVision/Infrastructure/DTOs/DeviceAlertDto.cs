using System;

namespace Infrastructure.DTOs
{
    public class DeviceAlertDto
    {
        public string DeviceSerialNumber { get; set; }
        public DateTime AlertMoment { get; set; }
        public string[] Parameters { get; set; }
    }
}
