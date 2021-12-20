using Core.Models;

namespace Infrastructure.Filtering.Specifications
{
    public class MeasurementSensorTypeSpecification : ISpecification<SensorMeasurement>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        private readonly SensorType? _sensorType;
        public MeasurementSensorTypeSpecification(SensorType? sensorType)
        {
            _sensorType = sensorType;
        }

        public bool IsSatisfied(SensorMeasurement t)
        {
            return _sensorType is null || t.SensorType == _sensorType;
        }
    }
}
