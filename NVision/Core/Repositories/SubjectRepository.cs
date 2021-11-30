using Core.Contexts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
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
    }
}
