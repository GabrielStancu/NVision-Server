using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<Subject> LoginAsync(string username, string password);
        Task<bool> ExistsUserAsync(string username);
    }
}