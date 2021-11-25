using Core.Contexts;
using Core.Models;

namespace Core.Repositories
{
    public class UserRepository<T> : GenericRepository<T>, IUserRepository<T> where T : User
    {
        public UserRepository(NVisionDbContext context) : base(context)
        {
        }
    }
}
