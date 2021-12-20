using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Filtering.Specifications;
using System.Collections.Generic;

namespace Infrastructure.Filtering.Filters
{
    public abstract class Filter<T> : IFilter<T> where T : Model
    {
        public bool IsOnCurrentPage(int index, int pageNumber, int pageSize)
        {
            int firstIndexOnPage = (pageNumber - 1) * pageSize;
            int lastIndexOnPage = pageNumber * pageSize - 1;

            return index >= firstIndexOnPage && index <= lastIndexOnPage;
        }

        public bool ExceededPage(int index, int pageNumber, int pageSize)
        {
            return index >= pageNumber * pageSize;
        }

        protected IEnumerable<T> FilterItems(
            IEnumerable<ISpecification<T>> specifications, 
            IEnumerable<T> unfilteredItems,
            SpecificationDto<T> specificationDto)
        {
            var andSpecification = new AndSpecification<T>(specifications);
            int index = 0;

            foreach (var item in unfilteredItems)
            {
                if (andSpecification.IsSatisfied(item))
                {
                    if (IsOnCurrentPage(index, specificationDto.PageNumber, specificationDto.PageSize))
                    {
                        yield return item;
                    }
                    index++;
                    if (ExceededPage(index, specificationDto.PageNumber, specificationDto.PageSize))
                    {
                        break;
                    }
                }
            }
        }
    }
}
