using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

    public class GenericRepository<T> : IGenericRepository<T> where T : Model
    {
        protected NVisionDbContext Context;

        public GenericRepository(NVisionDbContext context)
        {
            Context = context;
        }

        public async Task<T> SelectByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            bool alreadyStored = Context.Set<T>().Any(e => e.Equals(entity));

            if (!alreadyStored)
            {
                await Context.Set<T>().AddAsync(entity);
                await Context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await SelectByIdAsync(id);
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
