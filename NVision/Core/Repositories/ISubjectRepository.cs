using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<Subject> LoginAsync(string username, string password);
        Task<bool> ExistsUserAsync(string username);
        Task<IEnumerable<Subject>> GetWatcherSubjectsAsync(int watcherId);
        Task<Subject> GetSubjectWithMeasurementsAsync(int subjectId);
        Task<int> GetWatcherSubjectsCount(int watcherId);
        //////
        Task<IEnumerable<Subject>> GetWatcherDashboardSubjectsAsync(int watcherId);
    }
}