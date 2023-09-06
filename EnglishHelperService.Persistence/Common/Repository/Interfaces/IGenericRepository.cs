using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{

	/// <summary>
	/// Basic interface for a repository that deals with querying and modifying a specific set of entities.
	/// </summary>
	public interface IGenericRepository : IReadOnlyRepository
	{
		Task CreateAsync<TEntity>(TEntity entity) where TEntity : class, new();
		Task CreateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();
		Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class, new();
		Task UpdateAsync<TEntity>(
			 Expression<Func<TEntity, TEntity>> newValues,
			 Expression<Func<TEntity, bool>> where = null
		 ) where TEntity : class, new();
		Task UpdateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();
		Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, new();
		Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new();
		Task DeleteManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();
		Task SaveAsync();
	}
}