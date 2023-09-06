using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{
	/// <summary>
	/// Basic interface for a repositories.
	/// </summary>
	public interface IReadOnlyRepository
	{
		Task<TEntity> ReadAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new();
		IQueryable<TEntity> Query<TEntity>() where TEntity : class, new();
		IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new();
		IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) where TEntity : class, new();
		IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, out long totalCount) where TEntity : class, new();
		IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount) where TEntity : class, new();
		IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount) where TEntity : class, new();
		int Count<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new();
		Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new();
		long LongCount<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new();
		Task<long> LongCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new();
	}




	/// <summary>
	/// Basic interface for a repository that deals with querying and modifying a specific set of entities.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface IReadOnlyRepository<TEntity> where TEntity : class, new()
	{
		IQueryable<TEntity> EntitySet { get; }
		Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> where);
		IQueryable<TEntity> Query();
		IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter);
		IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
		IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, out long totalCount);
		IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount);
		IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount);
		int Count(Expression<Func<TEntity, bool>> filter = null);
		Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
		long LongCount(Expression<Func<TEntity, bool>> filter = null);
		Task<long> LongCountAsync(Expression<Func<TEntity, bool>> filter = null);
	}
}
