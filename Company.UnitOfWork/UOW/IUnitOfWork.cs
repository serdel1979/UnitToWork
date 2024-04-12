using Company.DB.Model;
using Company.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.UnitOfWork.UOW
{
    public interface IUnitOfWork
    {
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Worker> WorkerRepository { get; }
        Task SaveAsync();
    }
}
