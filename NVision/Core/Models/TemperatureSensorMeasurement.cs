namespace Core.Models
{
    public class TemperatureSensorMeasurement : SensorMeasurement
    {
        public new SensorType SensorType { get; } = SensorType.Temperature;
    }
}
