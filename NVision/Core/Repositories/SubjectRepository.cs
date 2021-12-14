using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class SubjectRepository : UserRepository<Subject>, ISubjectRepository
    {
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

        public async Task<IEnumerable<Subject>> GetWatcherSubjectsAsync(int watcherId)
        {
            return await Context.Subject
                .Where(s => s.WatcherId == watcherId)
                .ToListAsync();
        }

        public async Task<Subject> GetSubjectWithMeasurementsAsync(int subjectId)
        {
            return await Context.Subject
                .Include(s => s.SensorMeasurements)
                .FirstOrDefaultAsync(s => s.Id == subjectId);
        }

        public async Task<int> GetWatcherSubjectsCount(int watcherId)
        {
            var subjects = await Context.Subject
                .Where(s => s.WatcherId == watcherId)
                .ToListAsync();

            return subjects.Count;
        }
    }
}
