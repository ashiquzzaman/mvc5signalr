using System;
using System.Collections.Generic;

namespace SignalRMVCUnityCRUD.Models
{
    public class Department

    {
        public Int32 Id { set; get; }


        public String DepartmentName { set; get; }


        public virtual ICollection<Person> Persons { set; get; }

    }
}