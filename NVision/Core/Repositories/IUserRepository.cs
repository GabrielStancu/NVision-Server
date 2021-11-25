using Core.Models;

namespace Core.Repositories
{
    public interface IUserRepository<T> : IGenericRepository<T> where T : User
    {
    }
}