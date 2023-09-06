using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{

	#region GenericRepository with only dbContext

	/// <summary>
	/// EntityFramework based generic query class for database entities with dbContext.
	/// </summary>
	public class GenericRepository<TDbContext> : ReadOnlyRepository<TDbContext>, IGenericRepository
	where TDbContext : DbContext
	{
		/// <summary>
		/// A constructor that expects a database context.
		/// </summary>
		/// <param name="dbContext">database context</param>
		public GenericRepository(TDbContext dbContext) : base(dbContext) { }


		#region Read section

		/// <summary>
		/// Asynchronously reads the first entity matching the given criteria.
		/// </summary>
		/// <param name="where">Search terms</param>
		/// <returns>Entity instance of the given type, or null if none exists</returns>
		public override async Task<TEntity> ReadAsync<TEntity>(Expression<Func<TEntity, bool>> where)
		{
			return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(where);
		}

		/// <summary>
		///  Creates a query object for entities.
		/// </summary>
		public override IQueryable<TEntity> Query<TEntity>()
		{
			var baseQuery = dbContext.Set<TEntity>();

			return baseQuery.AsNoTracking();
		}

		/// <summary>
		/// Creates a query object for the entities that match the given filter expression
		/// </summary>
		/// <param name="filter">Filter expression</param>
		public override IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter)
		{
			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			return baseQuery.AsNoTracking();
		}

		/// <summary>
		///  Creates a query object for the entities that match the given filter expression in the specified order.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <param name="orderBy">Ordering expression</param>
		public override IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			baseQuery = orderBy == null ? baseQuery : orderBy(baseQuery);

			return baseQuery.AsNoTracking();
		}

		/// <summary>
		/// A query function designed to serve paged lists
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="totalCount">Total number of elements</param>
		public override IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, out long totalCount)
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = dbContext.Set<TEntity>();

			totalCount = baseQuery.LongCount();

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage).AsNoTracking();
		}

		/// <summary>
		/// A query function designed to serve paged lists with filtering.
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="filter">Filter expression</param>
		/// <param name="totalCount">Total number of elements</param>
		public override IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount)
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			totalCount = baseQuery.LongCount();

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage).AsNoTracking();
		}

		/// <summary>
		/// A query function designed to serve paged lists with filtering and ordering.
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="filter">Filter expression</param>
		/// <param name="orderBy">Ordering expression</param>
		/// <param name="totalCount">Total number of elements</param>
		public override IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount)
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			totalCount = baseQuery.LongCount();

			baseQuery = orderBy == null ? baseQuery : orderBy(baseQuery);

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage).AsNoTracking();
		}

		/// <summary>
		///  Counts the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression</returns>
		public override long Count<TEntity>(Expression<Func<TEntity, bool>> filter = null)
		{
			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			return baseQuery.LongCount();
		}

		#endregion



		#region Modification section


		/// <summary>
		/// Asynchronously saves the passed entity as a new record.
		/// </summary>
		/// <param name="entity">The entity to save</param>
		public virtual async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class, new()
		{
			dbContext.Add(entity);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Asynchronously saves passed entities as new records.
		/// </summary>
		/// <param name="entities">The entities to save</param>
		public virtual async Task CreateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
		{
			dbContext.AddRange(entities);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Asynchronously saves changes made to the passed entity.
		/// </summary>
		/// <param name="entity">The entity to update</param>
		public virtual async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class, new()
		{
			dbContext.Update(entity);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Asynchronously sets new values ​​to entities matching the criteria.
		/// </summary>
		/// <param name="newValues">New values</param>
		/// <param name="where">Filter condition for the items to be updated</param>
		public async Task UpdateAsync<TEntity>(Expression<Func<TEntity, TEntity>> newValues, Expression<Func<TEntity, bool>> where = null) where TEntity : class, new()
		{
			if (where != null)
			{
				var entitiesToUpdate = await dbContext.Set<TEntity>().Where(where).ToListAsync();

				foreach (var entity in entitiesToUpdate)
				{
					newValues.Compile()(entity);
					dbContext.Update(entity);
				}
			}
			else
			{
				var entitiesToUpdate = await dbContext.Set<TEntity>().ToListAsync();

				foreach (var entity in entitiesToUpdate)
				{
					newValues.Compile()(entity);
					dbContext.Update(entity);
				}
			}

			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///   Asynchronously saves changes made to the passed entities.
		/// </summary>
		/// <param name="entities">The entities to update</param>
		public virtual async Task UpdateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
		{
			foreach (var entity in entities)
			{
				dbContext.Update(entity);
			}

			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Deletes the given entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to delete</param>
		public virtual async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, new()
		{
			if (entity == null)
				return;

			dbContext.Entry(entity).State = EntityState.Deleted;

			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Asynchronously deletes entities matching the given conditions.
		/// </summary>
		/// <param name="where">Filter condition for the elements to be deleted</param>
		public virtual async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new()
		{
			var entitiesToDelete = where != null
		 ? await dbContext.Set<TEntity>().Where(where).ToListAsync()
		 : await dbContext.Set<TEntity>().ToListAsync();

			dbContext.RemoveRange(entitiesToDelete);
			await dbContext.SaveChangesAsync();
		}
		/// <summary>
		///  Deletes the given entities asynchronously.
		/// </summary>
		/// <param name="entities">The entities to delete</param>
		public virtual async Task DeleteManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
		{
			dbContext.RemoveRange(entities);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Saves the modification(s) made to the entity(s) asynchronously.
		/// </summary>
		public virtual async Task SaveAsync()
		{
			await dbContext.SaveChangesAsync();
		}

		#endregion
	}


	#endregion


	#region GenericRepository with dbContext and entity type

	/// <summary>
	/// EntityFramework based generic query class for database entities with dbContext and entity type.
	/// </summary>
	/// <typeparam name="TDbContext">Database type</typeparam>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public class GenericRepository<TDbContext, TEntity> : IRepository<TEntity>
		where TDbContext : DbContext
		where TEntity : class, new()
	{
		/// <summary>
		/// IGenericRepository type
		/// </summary>
		private readonly IGenericRepository _genericRepository;

		/// <summary>
		/// Current database context.
		/// </summary>
		protected TDbContext dbContext;

		/// <summary>
		/// A constructor that expects a database context.
		/// </summary>
		/// <param name="dbContext">database context</param>
		public GenericRepository(TDbContext dbContext)
		{
			dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_genericRepository = new GenericRepository<TDbContext>(dbContext);
		}


		#region Read section

		/// <summary>
		/// The entire queryable entity set.
		/// </summary>
		public virtual IQueryable<TEntity> EntitySet => dbContext.Set<TEntity>().AsNoTracking();

		/// <summary>
		/// Asynchronously reads the first entity matching the given criteria.
		/// </summary>
		/// <param name="where">Search terms</param>
		/// <returns>Entity instance of the given type, or null if none exists</returns>
		public virtual async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> where)
		{
			return await _genericRepository.ReadAsync(where);
		}


		/// <summary>
		///  Creates a query object for entities.
		/// </summary>
		public virtual IQueryable<TEntity> Query()
		{
			return _genericRepository.Query<TEntity>();
		}

		/// <summary>
		/// Creates a query object for the entities that match the given filter expression
		/// </summary>
		/// <param name="filter">Filter expression</param>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
		{
			return _genericRepository.Query(filter);
		}

		/// <summary>
		///  Creates a query object for the entities that match the given filter expression in the specified order.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <param name="orderBy">Ordering expression</param>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			return _genericRepository.Query(filter, orderBy);
		}

		/// <summary>
		/// A query function designed to serve paged lists
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="totalCount">Total number of elements</param>
		public virtual IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, out long totalCount)
		{
			return _genericRepository.PagedQuery<TEntity>(pageNumber, itemsOnPage, out totalCount);
		}

		/// <summary>
		/// A query function designed to serve paged lists with filtering.
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="filter">Filter expression</param>
		/// <param name="totalCount">Total number of elements</param>
		public virtual IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount)
		{
			return _genericRepository.PagedQuery(pageNumber, itemsOnPage, filter, out totalCount);
		}

		/// <summary>
		/// A query function designed to serve paged lists with filtering and ordering.
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="filter">Filter expression</param>
		/// <param name="orderBy">Ordering expression</param>
		/// <param name="totalCount">Total number of elements</param>
		public virtual IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount)
		{
			return _genericRepository.PagedQuery(pageNumber, itemsOnPage, filter, orderBy, out totalCount);
		}

		/// <summary>
		///  Counts the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression</returns>
		public virtual long Count(Expression<Func<TEntity, bool>> filter = null)
		{
			return _genericRepository.Count(filter);
		}


		#endregion


		#region Modification section

		/// <summary>
		/// Asynchronously saves the passed entity as a new record.
		/// </summary>
		/// <param name="entity">The entity to save</param>
		public virtual async Task CreateAsync(TEntity entity)
		{
			await _genericRepository.CreateAsync(entity);
		}

		/// <summary>
		/// Asynchronously saves passed entities as new records.
		/// </summary>
		/// <param name="entities">The entities to save</param>
		public virtual async Task CreateManyAsync(IEnumerable<TEntity> entities)
		{
			await _genericRepository.CreateManyAsync(entities);
		}



		/// <summary>
		/// Asynchronously saves changes made to the passed entity.
		/// </summary>
		/// <param name="entity">The entity to update</param>
		public virtual async Task UpdateAsync(TEntity entity)
		{
			await _genericRepository.UpdateAsync(entity);
		}

		/// <summary>
		///  Asynchronously sets new values ​​to entities matching the criteria.
		/// </summary>
		/// <param name="newValues">New values</param>
		/// <param name="where">Filter condition for the items to be updated</param>
		public virtual async Task UpdateAsync(Expression<Func<TEntity, TEntity>> newValues, Expression<Func<TEntity, bool>> where = null)
		{
			await _genericRepository.UpdateAsync(newValues, where);
		}

		/// <summary>
		///   Asynchronously saves changes made to the passed entities.
		/// </summary>
		/// <param name="entities">The entities to update</param>
		public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities)
		{
			await _genericRepository.UpdateManyAsync(entities);
		}

		/// <summary>
		///  Deletes the given entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to delete</param>
		public virtual async Task DeleteAsync(TEntity entity)
		{
			await _genericRepository.DeleteAsync(entity);
		}

		/// <summary>
		///  Asynchronously deletes entities matching the given conditions.
		/// </summary>
		/// <param name="where">Filter condition for the elements to be deleted</param>
		public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> where = null)
		{
			await _genericRepository.DeleteAsync(where);
		}

		/// <summary>
		///  Deletes the given entities asynchronously.
		/// </summary>
		/// <param name="entities">The entities to delete</param>
		public virtual async Task DeleteManyAsync(IEnumerable<TEntity> entities)
		{
			await _genericRepository.DeleteManyAsync(entities);
		}


		/// <summary>
		///  Saves the modification(s) made to the entity(s) asynchronously.
		/// </summary>
		public virtual async Task SaveAsync()
		{
			await dbContext.SaveChangesAsync();
		}

		#endregion
	}

	#endregion
}