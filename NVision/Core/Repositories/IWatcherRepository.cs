using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IWatcherRepository : IGenericRepository<Watcher>
    {
        Task<Watcher> LoginAsync(string username, string password);
        Task<bool> ExistsUserAsync(string username);
        Task<string> GetWatcherPhoneNumberByIdAsync(int id);
    }
}