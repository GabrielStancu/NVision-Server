using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<Subject> LoginAsync(string username, string password);
        Task<bool> ExistsUserAsync(string username);
        Task<bool> ExistsUserAsync(int id, string username);
        Task<IEnumerable<Subject>> GetWatcherSubjectsAsync(int watcherId);
        Task<int> GetWatcherSubjectsCountAsync(int watcherId);
        Task<IEnumerable<Subject>> GetWatcherDashboardSubjectsAsync(int watcherId);
    }

    public class SubjectRepository : UserRepository<Subject>, ISubjectRepository
    {
        private const int _watcherDashboardSubjectsCount = 10;
        public SubjectRepository(NVisionDbContext context) : base(context)
        {
        }
        public override async Task<Subject> LoginAsync(string username, string password)
        {
            return await Context.Subject
                .FirstOrDefaultAsync(s => s.Username.Equals(username) &&
                                          s.Password.Equals(password));
        }

        public override async Task<bool> ExistsUserAsync(string username)
        {
            var subject = await Context.Subject
                .FirstOrDefaultAsync(s => s.Username.Equals(username));

            return subject != null;
        }

        public override async Task<bool> ExistsUserAsync(int id, string username)
        {
            var subject = await Context.Subject
                .FirstOrDefaultAsync(s => s.Username.Equals(username) && s.Id != id);

            return subject != null;
        }

        public async Task<IEnumerable<Subject>> GetWatcherSubjectsAsync(int watcherId)
        {
            return await Context.Subject
                .Where(s => s.WatcherId == watcherId)
                .ToListAsync();
        }

        public async Task<int> GetWatcherSubjectsCountAsync(int watcherId)
        {
            var subjects = await Context.Subject
                .Where(s => s.WatcherId == watcherId)
                .ToListAsync();

            return subjects.Count;
        }

        public async Task<IEnumerable<Subject>> GetWatcherDashboardSubjectsAsync(int watcherId)
        {
            var subjects = await Context.Subject
                //.Include(s => s.SensorMeasurements.OrderByDescending(sm => sm.Timestamp).Take(1))
                .Where(s => s.WatcherId == watcherId)
                //.OrderByDescending(s => s.SensorMeasurements.FirstOrDefault().Timestamp)
                .Take(_watcherDashboardSubjectsCount)
                .ToListAsync();
            return subjects;
        }
    }
}
