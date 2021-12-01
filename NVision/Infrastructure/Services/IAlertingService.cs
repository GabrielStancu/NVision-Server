using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAlertingService
    {
        Task<HealthAlert> CreateAlertAsync(AlertDto alertDto);
    }
}