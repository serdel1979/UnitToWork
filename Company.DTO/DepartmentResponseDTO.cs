using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DTO
{
    public class DepartmentResponseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<WorkerResponseDTO> Workers { get; set; }
    }
}
