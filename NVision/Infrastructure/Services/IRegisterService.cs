using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IRegisterService
    {
        Task<bool> RegisterUserAsync(WatcherRegisterRequestDto watcherRegisterRequestDto);
        Task<bool> RegisterUserAsync(SubjectRegisterRequestDto subjectRegisterRequestDto);
    }
}