using Company.DTO;
using Company.UnitOfWork.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Strategy
{
    public class DepartmentContext
    {
        private IStrategy _strategy;

        public IStrategy Strategy { 
            set { _strategy = value; }
        }

        public DepartmentContext(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task Add(WorkerDTO worker, IUnitOfWork unitOfWork)
        {
           await _strategy.Add(worker, unitOfWork);
        }

    }
}
