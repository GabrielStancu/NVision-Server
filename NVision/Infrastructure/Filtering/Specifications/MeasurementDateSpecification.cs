using Core.Models;
using System;

namespace Infrastructure.Filtering.Specifications
{
    public class MeasurementDateSpecification : ISpecification<SensorMeasurement>
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public MeasurementDateSpecification(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }
        public bool IsSatisfied(SensorMeasurement t)
        {
            return t.Timestamp >= _startDate && t.Timestamp <= _endDate;
        }
    }
}
