using System.Linq;
using SignalRMVCUnityCRUD.Models;

namespace SignalRMVCUnityCRUD.Managers
{
   public interface IDepertmentManager
    {
       IQueryable<Department> GetAll();
       Department Get(int id);
       Department Create(Department model);
       int Update(Department model);
       int Delete(int id);
    }
}
