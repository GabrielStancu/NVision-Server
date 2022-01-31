using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class SensorMeasurement : Model
    {
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [InverseProperty("SensorMeasurements")]
        public Subject Subject { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public SensorType SensorType { get; }
        public SensorMeasurement(SensorType sensorType)
        {
            SensorType = sensorType;
        }
        public SensorMeasurement()
        {
        }
    }
}
