using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using repository.Entities;
using repository.Imp.StoreContext;
using repository.Interfaces;

namespace repository.Imp.Repository
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entities;

        public Repository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return _entities.AsQueryable();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filterExpression)
        {
            return (int)await _entities.CountAsync(filterExpression);
        }

        public async void DeleteByIdAsync(Guid Id)
        {
            if(Id == null) throw new ArgumentNullException("Entity");
            T entity = await _entities.Where(x => x.Id == Id).FirstOrDefaultAsync(); 
            if(entity == null) throw new ArgumentNullException("Not found");
            _entities.Remove(entity);
        }

        public void DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            _context.RemoveRange(filterExpression);
        }

        public void DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            _context.Remove(filterExpression);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _entities.AnyAsync(filterExpression);
        }

        public async Task<IEnumerable<T>> FilterBy(Expression<Func<T, bool>> filterExpression)
        {
            var query = _entities.Where(filterExpression);
            return await query.ToListAsync();
        }

        public  IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjected>> projectionExpression)
        {
            return _entities.Where(filterExpression)
                .Select(projectionExpression)
                .ToList();
        }


        public async Task<TProjected> FilterOneAsync<TProjected>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjected>> projectionExpression)
        {

            return await _entities.Where(filterExpression).Select(projectionExpression).FirstOrDefaultAsync();

        }

        public async Task<T> FindByIdAsync(Guid Id)
        {
            return await _entities.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

       


        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression, List<Expression<Func<T, object>>> children)
        {
            IQueryable<T> query = _entities;
            if(children != null) {
                query = children.Aggregate(query, (current, include)=> current.Include(include));
            }
            return await query.Where(filterExpression).FirstOrDefaultAsync();
        }


          public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderBy, List<Expression<Func<T, object>>> children, int PageNumber, int PageSize)
        {

            IQueryable<T> query = _entities;
            if(filterExpression != null) _entities.Where(filterExpression);

            if (children != null)
            {
                query = children.Aggregate(query, 
                  (current, include) => current.Include(include));
            }

            if(orderBy != null) query = query.OrderBy(orderBy);


            return await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToArrayAsync();
        }

        public void InsertManyAsync(ICollection<T> documents)
        {
            _entities.AddRangeAsync(documents);
        }

        public void InsertOneAsync(T document)
        {
            _entities.AddAsync(document);
        }

        public async void ReplaceOneAsync(Guid Id, T document)
        {
            _entities.Update(document);
        }
    }
}
