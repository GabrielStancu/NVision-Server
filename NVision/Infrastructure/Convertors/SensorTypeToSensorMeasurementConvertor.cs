using Core.Models;

namespace Infrastructure.Convertors
{
    public class SensorTypeToSensorMeasurementConvertor : ISensorTypeToSensorMeasurementConvertor
    {
        public SensorMeasurement Convert(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Airflow:
                    return new AirflowSensorMeasurement();
                case SensorType.BloodPressure:
                    return new BloodPressureSensorMeasurement();
                case SensorType.ECG:
                    return new EcgSensorMeasurement();
                case SensorType.GSR:
                    return new GsrSensorMeasurement();
                case SensorType.PulseOxygenHeartRate:
                    return new PulseOxygenHeartRateSensorMeasurement();
                case SensorType.Temperature:
                    return new TemperatureSensorMeasurement();
                default:
                    return null;
            }
        }
    }
}
