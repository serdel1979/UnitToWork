using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DB.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
