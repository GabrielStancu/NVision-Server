using Core.Models;

namespace Infrastructure.Filtering.Specifications
{
    public class AlertSpecification : ISpecification<Alert>
    {
        public bool IsSatisfied(Alert t)
        {
            return true;
        }
    }
}
