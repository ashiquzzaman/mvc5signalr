using System.Data.Entity;
using SignalRMVCUnityCRUD.Models;

namespace SignalRMVCUnityCRUD.Repositories
{
    public class DepertmentRepository : Repository<Department>, IDepertmentRepository
    {
        public DepertmentRepository(DbContext context) : base(context)
        {
        }
    }
}