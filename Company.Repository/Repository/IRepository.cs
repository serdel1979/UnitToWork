using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repository
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity,bool>>?filter=null);
        Task<IEnumerable<TEntity>> GetAll();
        Task Create(TEntity entity);
        Task Delete(TEntity data);
        Task Update(TEntity data);
        Task Save();
    }
}
