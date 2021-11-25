using Core.Contexts;
using Core.Models;

namespace Core.Repositories
{
    public class WatcherRepository : UserRepository<Watcher>, IWatcherRepository
    {
        public WatcherRepository(NVisionDbContext context) : base(context)
        {
        }
    }
}
