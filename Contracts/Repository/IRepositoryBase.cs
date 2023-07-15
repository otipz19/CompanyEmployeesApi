using System.Linq.Expressions;

namespace Contracts.Repository
{
    public interface IRepositoryBase<T>
        where T : class
    {
        public IQueryable<T> GetAll(bool asNoTracking = false);

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool asNoTracking = false);

        public void Create(T entity);

        public void Update(T entity);

        public void Delete(T entity);
    }
}
