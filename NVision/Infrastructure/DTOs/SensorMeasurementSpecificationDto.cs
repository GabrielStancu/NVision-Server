using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class SensorMeasurementSpecificationDto : SpecificationDto<SensorMeasurement>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SensorType SensorType { get; set; }
    }
}
