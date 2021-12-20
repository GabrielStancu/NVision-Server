using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Filtering.Specifications;
using System.Collections.Generic;

namespace Infrastructure.Filtering.Filters
{
    public class SubjectFilter : Filter<Subject>, ISubjectFilter
    {
        public IEnumerable<Subject> Filter(IEnumerable<Subject> unfilteredItems, SubjectSpecificationDto specificationDto)
        {
            var specifications = new List<ISpecification<Subject>>
            {
                new SubjectNameSpecification(specificationDto.SubjectName)
            };

            return FilterItems(specifications, unfilteredItems, specificationDto);
        }
    }
}
