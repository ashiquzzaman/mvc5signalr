using System.Data.Entity;
using SignalRMVCUnityCRUD.Models;

namespace SignalRMVCUnityCRUD.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}