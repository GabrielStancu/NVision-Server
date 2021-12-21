using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ISubjectDataService
    {
        Task<SubjectWithMeasurementsReplyDto> GetSubjectWithMeasurementsAsync(SubjectWithMeasurementsRequestDto request);
    }
}