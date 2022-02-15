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
        /// <summary>
        /// The constructor that should be used across the app
        /// </summary>
        public SensorMeasurement(SensorType sensorType)
        {
            SensorType = sensorType;
        }
        /// <summary>
        /// Used by EF-Core. This constructor should not be used across the app
        /// </summary>
        public SensorMeasurement()
        {

        }
    }
}
