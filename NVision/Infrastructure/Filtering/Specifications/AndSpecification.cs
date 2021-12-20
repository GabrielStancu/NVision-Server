using System.Collections.Generic;

namespace Infrastructure.Filtering.Specifications
{
    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly IEnumerable<ISpecification<T>> _specifications;

        public AndSpecification(IEnumerable<ISpecification<T>> specifications) 
        {
            _specifications = specifications;
        }

        public bool IsSatisfied(T t)
        {
            foreach (var specification in _specifications)
            {
                if (!specification.IsSatisfied(t))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
