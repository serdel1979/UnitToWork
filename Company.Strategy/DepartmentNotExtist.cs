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
    public class DepartmentNotExtist : IStrategy
    {
        public async Task Add(WorkerDTO worker, IUnitOfWork unitOfWork)
        {
            var department = new Department()
            {
                Name = worker.Department.Name,
            };
            await unitOfWork.DepartmentRepository.Create(department);
            var wornerNew = new Worker()
            {
                Name = worker.Name,
                LastName = worker.LastName,
                Department = department,
            };
            await unitOfWork.WorkerRepository.Create(wornerNew);
            await unitOfWork.SaveAsync();
        }
    }
}
