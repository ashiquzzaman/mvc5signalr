using System.Data.Entity;
using System.Linq;
using SignalRMVCUnityCRUD.Models;
using SignalRMVCUnityCRUD.Repositories;

namespace SignalRMVCUnityCRUD.Managers
{
    public class DepertmentManager: IDepertmentManager
    {
        IDepertmentRepository _depertment;

        public DepertmentManager(IDepertmentRepository depertment)
        {
            _depertment = depertment;
        }


        public IQueryable<Department> GetAll()
        {
            return _depertment.All().Include(d=>d.Persons);
        }
        public Department Get(int id)
        {
            return _depertment.Find(d=>d.Id==id);
        }
        public Department Create(Department model)
        {
            return _depertment.Create(model);
        }
        public int Update(Department model)
        {
            return _depertment.Update(model);
        }

        public int Delete(int id)
        {
            return _depertment.Delete(d=>d.Id==id);
        }

    }
}