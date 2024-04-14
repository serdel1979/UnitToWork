using Company.DB.Model;
using Company.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public interface IDepartmentService
    {
        IQueryable<DepartmentDTO> FindByName(string Name);
        Task New(DepartmentDTO department);
        Task Delete(int Id);
        Task Update(DepartmentDTO department, int Id);
        Task<DepartmentResponseDTO> Get(int Id);
        Task<IEnumerable<DepartmentDTO>> GetAll();
    }
}
