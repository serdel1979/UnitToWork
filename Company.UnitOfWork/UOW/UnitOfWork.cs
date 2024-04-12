using Company.DB;
using Company.DB.Model;
using Company.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.UnitOfWork.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextCompany _context;

        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Worker> WorkerRepository { get; }

        public UnitOfWork(ContextCompany context)
        {
            this._context = context;
            DepartmentRepository = new Repository<Department>(_context);
            WorkerRepository = new Repository<Worker>(_context);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
