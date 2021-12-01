using Core.Contexts;
using Core.Models;

namespace Core.Repositories
{
    public class AlertRepository : GenericRepository<Alert>, IAlertRepository
    {
        public AlertRepository(NVisionDbContext context) : base(context)
        {
        }
    }
}
