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
    public interface IStrategy
    {
        public Task Add(WorkerDTO worker, IUnitOfWork unitOfWork);
    }
}
