using Core.Contexts;
using Core.Models;

namespace Core.Repositories
{
    public class SubjectRepository : UserRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(NVisionDbContext context) : base(context)
        {
        }
    }
}
