using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IWatcherRepository : IGenericRepository<Watcher>
    {
        Task<Watcher> LoginAsync(string username, string password);
        Task<bool> ExistsUserAsync(string username);
        Task<string> GetWatcherPhoneNumberByIdAsync(int id);
    }

    public class WatcherRepository : UserRepository<Watcher>, IWatcherRepository
    {
        public WatcherRepository(NVisionDbContext context) : base(context)
        {
        }

        public override async Task<Watcher> LoginAsync(string username, string password)
        {
            return await Context.Watcher
                .FirstOrDefaultAsync(w => w.Username.Equals(username) &&
                                          w.Password.Equals(password));
        }

        public override async Task<bool> ExistsUserAsync(string username)
        {
            var watcher = await Context.Watcher
                .FirstOrDefaultAsync(w => w.Username.Equals(username));

            return watcher != null;
        }

        public async Task<string> GetWatcherPhoneNumberByIdAsync(int id)
        {
            var watcher = await SelectByIdAsync(id);
            return watcher.PhoneNumber;
        }
    }
}
