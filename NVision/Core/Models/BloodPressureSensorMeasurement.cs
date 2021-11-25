namespace Core.Models
{
    public class BloodPressureSensorMeasurement : SensorMeasurement
    {
        public new SensorType SensorType { get; } = SensorType.BloodPressure;
    }
}
