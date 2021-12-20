namespace Infrastructure.Filtering.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
}
