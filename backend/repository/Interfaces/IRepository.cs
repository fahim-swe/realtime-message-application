using System.Linq.Expressions;
using repository.Entities;

namespace repository.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        IQueryable<TEntity> AsQueryable();

        Task<IEnumerable<TEntity>> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TProjected>> projectionExpression);
        Task<TProjected> FilterOneAsync<TProjected>(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TProjected>> projectionExpression);
     
     
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression, List<Expression<Func<TEntity, object>>> children = null);
        Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, Object>> orderBy, List<Expression<Func<TEntity, object>>> children,int PageNumber, int PageSize);
        Task<TEntity> FindByIdAsync(Guid Id);


        Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filterExpression);


        void InsertOneAsync(TEntity document);
        void InsertManyAsync(ICollection<TEntity> documents);


        void ReplaceOneAsync(Guid Id, TEntity document);


        void DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);
        void DeleteByIdAsync(Guid Id);

        
        void DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
    }
}