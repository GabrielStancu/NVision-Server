using Core.Models;

namespace Infrastructure.DTOs
{
    public class SubjectSpecificationDto : SpecificationDto<Subject>
    {
        public string SubjectName { get; set; }
    }
}
