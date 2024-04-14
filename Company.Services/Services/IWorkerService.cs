using Company.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public interface IWorkerService
    {
        IQueryable<WorkerDTO> FindByName(string Name);
        Task New(WorkerDTO department);
        Task Delete(int Id);
        Task Update(WorkerDTO worker, int Id);
        Task<WorkerDTO> Get(int Id);
        Task<IEnumerable<WorkerDTO>> GetAll();
    }
}
