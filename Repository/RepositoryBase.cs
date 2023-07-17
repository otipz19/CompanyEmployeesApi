using Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        private readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll(bool asNoTracking = false)
        {
            var set = _context.Set<T>();
            return asNoTracking ? set.AsNoTracking() : set;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool asNoTracking = false)
        {
            var set = _context.Set<T>().Where(expression);
            return asNoTracking ? set.AsNoTracking() : set;
        }
    }
}
