using Company.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ContextCompany _context;
        private DbSet<TEntity> _dbSet;

        public Repository(ContextCompany context)
        {
            this._context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            _context.Add(entity);
        }

        public async Task Delete(TEntity data)
        {
            _dbSet.Remove(data);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> finds = filter == null ? _dbSet : _dbSet.Where(filter);
            return finds;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }



        public async Task Update(TEntity data)
        {
            _dbSet.Update(data);
        }
    }
}
