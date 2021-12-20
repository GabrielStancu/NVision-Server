using Core.Models;
using Infrastructure.DTOs;
using System.Collections.Generic;

namespace Infrastructure.Filtering.Filters
{
    public interface ISubjectFilter : IFilter<Subject>
    {
        IEnumerable<Subject> Filter(IEnumerable<Subject> unfilteredItems, SubjectSpecificationDto specificationDto);
    }
}
