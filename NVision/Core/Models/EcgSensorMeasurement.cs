namespace Core.Models
{
    public class EcgSensorMeasurement : SensorMeasurement
    {
        public new SensorType SensorType { get; } = SensorType.ECG;
    }
}
