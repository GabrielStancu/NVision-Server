using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Core.Repositories
{
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
    }
}
