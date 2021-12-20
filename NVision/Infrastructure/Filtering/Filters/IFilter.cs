namespace Infrastructure.Filtering.Filters
{
    public interface IFilter<T>
    {
        public bool ExceededPage(int index, int pageNumber, int pageSize);
        public bool IsOnCurrentPage(int index, int pageNumber, int pageSize);
    }
}