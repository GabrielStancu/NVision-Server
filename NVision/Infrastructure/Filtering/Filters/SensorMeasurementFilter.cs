using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Filtering.Specifications;
using System.Collections.Generic;

namespace Infrastructure.Filtering.Filters
{
    public class SensorMeasurementFilter : Filter<SensorMeasurement>, ISensorMeasurementFilter
    {
        public IEnumerable<SensorMeasurement> Filter(IEnumerable<SensorMeasurement> unfilteredItems, SensorMeasurementSpecificationDto specificationDto)
        {
            var specifications = new List<ISpecification<SensorMeasurement>>
            {
                new MeasurementDateSpecification(specificationDto.StartDate, specificationDto.EndDate),
                new MeasurementSensorTypeSpecification(specificationDto.SensorType)
            };

            return FilterItems(specifications, unfilteredItems, specificationDto);
        }
    }
}
