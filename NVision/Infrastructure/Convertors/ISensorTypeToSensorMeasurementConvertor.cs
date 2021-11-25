using Core.Models;

namespace Infrastructure.Convertors
{
    public interface ISensorTypeToSensorMeasurementConvertor
    {
        SensorMeasurement Convert(SensorType sensorType);
    }
}