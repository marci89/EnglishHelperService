using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{
	/// <summary>
	///Basic interface for repositories.
	/// </summary>
	public interface IRepository
	{
		Task SaveAsync();
	}

	/// <summary>
	/// Basic interface for a repository that deals with querying and modifying a specific set of entities.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface IRepository<TEntity> : IRepository, IReadOnlyRepository<TEntity> where TEntity : class, new()
	{
		Task CreateAsync(TEntity entity);
		Task CreateManyAsync(IEnumerable<TEntity> entities);
		Task UpdateAsync(TEntity entity);
		Task UpdateAsync(Expression<Func<TEntity, TEntity>> newValues, Expression<Func<TEntity, bool>> where = null);
		Task UpdateManyAsync(IEnumerable<TEntity> entities);
		Task DeleteAsync(TEntity entity);
		Task DeleteAsync(Expression<Func<TEntity, bool>> where);
		Task DeleteManyAsync(IEnumerable<TEntity> entities);
	}
}