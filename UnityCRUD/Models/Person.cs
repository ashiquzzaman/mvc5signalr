using System;

namespace SignalRMVCUnityCRUD.Models
{
    public class Person
    {
        public Int32 Id { set; get; }


        public String Name { set; get; }


        public virtual Department Department { get; set; }


        public Int32 DepartmentId { set; get; }

        public string Gender { set; get; }
    }
}