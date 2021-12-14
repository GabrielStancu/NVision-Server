using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherDataService
    {
        Task<SubjectWithMeasurementsDto> GetSubjectWithMeasurementsAsync(int subjectId);
        Task<WatcherHomepageDataDto> GetWatcherHomepageDataAsync(int watcherId);
    }
}