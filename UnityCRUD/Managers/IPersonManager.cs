using System.Linq;
using SignalRMVCUnityCRUD.Models;

namespace SignalRMVCUnityCRUD.Managers
{
    public interface IPersonManager
    {
        IQueryable<Person> GetAll();
        Person Get(int id);
        Person Create(Person model);
        int Update(Person model);
    }
}
