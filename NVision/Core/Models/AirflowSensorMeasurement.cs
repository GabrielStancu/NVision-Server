namespace Core.Models
{
    public class AirflowSensorMeasurement : SensorMeasurement 
    {
        public new SensorType SensorType { get; } = SensorType.Airflow;
    }
}
