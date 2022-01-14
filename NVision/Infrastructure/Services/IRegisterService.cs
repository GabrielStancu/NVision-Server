using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IRegisterService
    {
        Task<bool> RegisterUserAsync(UserRegisterRequestDto userRegisterRequestDto);
    }
}