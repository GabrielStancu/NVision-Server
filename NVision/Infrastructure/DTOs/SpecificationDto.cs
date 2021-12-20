using Core.Models;

namespace Infrastructure.DTOs
{
    public class SpecificationDto<T> where T : Model
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
