namespace Core.Models
{
    public class GsrSensorMeasurement : SensorMeasurement
    {
        public new SensorType SensorType { get; } = SensorType.GSR;
    }
}
