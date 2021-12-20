using Core.Models;
using Infrastructure.DTOs;
using System.Collections.Generic;

namespace Infrastructure.Filtering.Filters
{
    public interface ISensorMeasurementFilter
    {
        IEnumerable<SensorMeasurement> Filter(IEnumerable<SensorMeasurement> unfilteredItems, SensorMeasurementSpecificationDto specificationDto);
    }
}