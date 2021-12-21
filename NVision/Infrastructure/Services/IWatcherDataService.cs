using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherDataService
    {
        Task<WatcherHomepageDataReplyDto> GetWatcherHomepageDataAsync(WatcherHomepageDataRequestDto request);
        Task<AlertDto> AnswerAndGetNextAlertAsync(AlertAnswerDto alertAnswerDto);
    }
}