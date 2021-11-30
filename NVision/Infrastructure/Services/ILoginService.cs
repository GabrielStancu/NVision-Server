using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ILoginService
    {
        Task<LoginResultDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}