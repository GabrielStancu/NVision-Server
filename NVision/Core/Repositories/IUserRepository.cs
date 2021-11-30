using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRepository<T> : IGenericRepository<T> where T : User
    {
        Task<T> LoginAsync(string username, string password);
        Task<bool> ExistsUserAsync(string username);
    }
}