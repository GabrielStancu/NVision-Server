namespace Core.Models
{
    public class PulseOxygenHeartRateSensorMeasurement : SensorMeasurement
    {
        public new SensorType SensorType { get; } = SensorType.PulseOxygenHeartRate;
    }
}
