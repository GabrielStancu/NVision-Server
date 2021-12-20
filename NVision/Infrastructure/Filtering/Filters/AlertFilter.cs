using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Filtering.Specifications;
using System.Collections.Generic;

namespace Infrastructure.Filtering.Filters
{
    public class AlertFilter : Filter<Alert>, IAlertFilter
    {
        public IEnumerable<Alert> Filter(IEnumerable<Alert> unfilteredItems, AlertSpecificationDto specificationDto)
        {
            var specifications = new List<ISpecification<Alert>>
            {
                new AlertSpecification()
            };

            return FilterItems(specifications, unfilteredItems, specificationDto);
        }
    }
}
