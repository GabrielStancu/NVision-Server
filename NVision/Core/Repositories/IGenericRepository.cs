using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IGenericRepository<T> where T : Model
    {
        Task DeleteAsync(int id);
        Task InsertAsync(T entity);
        Task<IEnumerable<T>> SelectAllAsync();
        Task<T> SelectByIdAsync(int id);
        Task UpdateAsync(T entity);
    }
}