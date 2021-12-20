using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherDataService
    {
        Task<SubjectWithMeasurementsReplyDto> GetSubjectWithMeasurementsAsync(SubjectWithMeasurementsRequestDto request);
        Task<WatcherHomepageDataReplyDto> GetWatcherHomepageDataAsync(WatcherHomepageDataRequestDto request);
    }
}