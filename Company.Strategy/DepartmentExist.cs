using Company.DB.Model;
using Company.DTO;
using Company.UnitOfWork.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Strategy
{
    public class DepartmentExist : IStrategy
    {
        public async Task Add(WorkerDTO worker, IUnitOfWork unitOfWork)
        {
            var workerEntity = new Worker()
            {
                Name = worker.Name,
                LastName = worker.LastName,
                DepartmentId = worker.DepartmentId,
            };
            await unitOfWork.WorkerRepository.Create(workerEntity);
            await unitOfWork.SaveAsync();
        }
    }
}
