using Core.Contexts;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public abstract class UserRepository<T> : GenericRepository<T>, IUserRepository<T> where T : User
    {
        public UserRepository(NVisionDbContext context) : base(context)
        {
        }

        public abstract Task<T> LoginAsync(string username, string password);
        public abstract Task<bool> ExistsUserAsync(string username);
    }
}
