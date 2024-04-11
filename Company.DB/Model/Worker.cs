using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DB.Model
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
