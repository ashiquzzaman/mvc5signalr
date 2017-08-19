using System.Linq;
using SignalRMVCUnityCRUD.Models;
using SignalRMVCUnityCRUD.Repositories;

namespace SignalRMVCUnityCRUD.Managers
{
    public class PersonManager: IPersonManager
    {
        private IPersonRepository _person;

        public PersonManager(IPersonRepository person)
        {
            _person = person;
        }


        public IQueryable<Person> GetAll()
        {
            return _person.All();
        }
        public Person Get(int id)
        {
            return _person.Find(d => d.Id == id);
        }
        public Person Create(Person model)
        {
            return _person.Create(model);
        }
        public int Update(Person model)
        {
            return _person.Update(model);
        }
    }
}