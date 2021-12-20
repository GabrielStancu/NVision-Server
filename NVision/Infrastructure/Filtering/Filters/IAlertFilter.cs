using Core.Models;
using Infrastructure.DTOs;
using System.Collections.Generic;

namespace Infrastructure.Filtering.Filters
{
    public interface IAlertFilter : IFilter<Alert>
    {
        IEnumerable<Alert> Filter(IEnumerable<Alert> unfilteredItems, AlertSpecificationDto specificationDto);
    }
}
