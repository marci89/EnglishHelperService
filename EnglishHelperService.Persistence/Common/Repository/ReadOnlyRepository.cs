using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EnglishHelperService.Persistence.Common
{

	#region GenericRepository with only dbContext

	/// <summary>
	/// EntityFramework based generic readonly query class for database entities with dbContext.
	/// </summary>
	public class ReadOnlyRepository<TDbContext> : IReadOnlyRepository
	   where TDbContext : DbContext
	{

		/// <summary>
		/// Current database context.
		/// </summary>
		protected TDbContext dbContext;


		/// <summary>
		/// A constructor that expects a database context.
		/// </summary>
		/// <param name="dbContext">database context</param>
		public ReadOnlyRepository(TDbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		/// <summary>
		/// Asynchronously reads the first entity matching the given criteria.
		/// </summary>
		/// <param name="where">Search terms</param>
		public virtual async Task<TEntity> ReadAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new()
		{
			return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(where);
		}

		/// <summary>
		///  Creates a query object for entities.
		/// </summary>
		public virtual IQueryable<TEntity> Query<TEntity>() where TEntity : class, new()
		{
			return dbContext.Set<TEntity>().AsNoTracking();
		}

		/// <summary>
		/// Creates a query object for the entities that match the given filter expression
		/// </summary>
		/// <param name="filter">Filter expression</param>
		public virtual IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
		{
			return dbContext.Set<TEntity>().Where(filter).AsNoTracking();
		}

		/// <summary>
		///  Creates a query object for the entities that match the given filter expression in the specified order.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <param name="orderBy">Ordering expression</param>
		public virtual IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) where TEntity : class, new()
		{
			var query = dbContext.Set<TEntity>().Where(filter);

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			return query.AsNoTracking();
		}

		/// <summary>
		/// A query function designed to serve paged lists
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="totalCount">Total number of elements</param>
		public virtual IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, out long totalCount) where TEntity : class, new()
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = dbContext.Set<TEntity>();

			totalCount = baseQuery.LongCount();

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage);
		}

		/// <summary>
		/// A query function designed to serve paged lists with filtering.
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="pageNumber">Page number to retrieve(starting from 1)</param>
		/// <param name="itemsOnPage">Number of elements per page</param>
		/// <param name="filter">Filter expression</param>
		/// <param name="totalCount">Total number of elements</param>
		public virtual IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount) where TEntity : class, new()
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			totalCount = baseQuery.LongCount();

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage);
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
		public virtual IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount) where TEntity : class, new()
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			totalCount = baseQuery.LongCount();

			baseQuery = orderBy == null ? baseQuery : orderBy(baseQuery);

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage);
		}

		/// <summary>
		///  Counts in int the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (int)</returns>
		public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new()
		{
			var query = dbContext.Set<TEntity>().AsQueryable();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.Count();
		}

		/// <summary>
		///  Asynchronously counts in int the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (int)</returns>
		public virtual async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new()
		{
			var query = dbContext.Set<TEntity>().AsQueryable();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return await query.CountAsync();
		}

		/// <summary>
		///  Counts in long the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (long)</returns>
		public virtual long LongCount<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new()
		{
			var query = dbContext.Set<TEntity>().AsQueryable();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.LongCount();
		}

		/// <summary>
		///  Asynchronously counts in long the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (long)</returns>
		public virtual async Task<long> LongCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new()
		{
			var query = dbContext.Set<TEntity>().AsQueryable();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return await query.LongCountAsync();
		}
	}



	#endregion


	#region GenericRepository with dbContext and entity type

	/// <summary>
	/// EntityFramework based generic readonly query class for database entities with dbContext and entity type.
	/// </summary>
	public class ReadOnlyRepository<TDbContext, TEntity> : IReadOnlyRepository<TEntity>
		where TDbContext : DbContext
		where TEntity : class, new()
	{
		/// <summary>
		/// IReadOnlyRepository
		/// </summary>
		protected readonly IReadOnlyRepository readOnlyRepository;


		/// <summary>
		/// Current database context.
		/// </summary>
		protected TDbContext _dbContext;

		/// <summary>
		/// A constructor that expects a database context.
		/// </summary>
		/// <param name="dbContext">database context</param>
		public ReadOnlyRepository(TDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			readOnlyRepository = new ReadOnlyRepository<TDbContext>(dbContext);
		}

		/// <summary>
		/// A lekérdezhető teljes entitás halmaz.
		/// </summary>
		public virtual IQueryable<TEntity> EntitySet => _dbContext.Set<TEntity>().AsNoTracking();

		/// <summary>
		/// Asynchronously reads the first entity matching the given criteria.
		/// </summary>
		/// <param name="where">Search terms</param>
		/// <returns>Entity instance of the given type, or null if none exists</returns>
		public virtual async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> where)
		{
			return await readOnlyRepository.ReadAsync(where);
		}

		/// <summary>
		///  Creates a query object for entities.
		/// </summary>
		public virtual IQueryable<TEntity> Query()
		{
			return readOnlyRepository.Query<TEntity>();
		}

		/// <summary>
		/// Creates a query object for the entities that match the given filter expression
		/// </summary>
		/// <param name="filter">Filter expression</param>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
		{
			return readOnlyRepository.Query<TEntity>(filter);
		}

		/// <summary>
		///  Creates a query object for the entities that match the given filter expression in the specified order.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <param name="orderBy">Ordering expression</param>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			return readOnlyRepository.Query<TEntity>(filter, orderBy);
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
			return readOnlyRepository.PagedQuery<TEntity>(pageNumber, itemsOnPage, out totalCount);
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
			return readOnlyRepository.PagedQuery(pageNumber, itemsOnPage, filter, out totalCount);
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
			return readOnlyRepository.PagedQuery(pageNumber, itemsOnPage, filter, orderBy, out totalCount);
		}

		/// <summary>
		/// Counts in int the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (int)</returns>
		public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
		{
			return readOnlyRepository.Count(filter);
		}

		/// <summary>
		///  Asynchronously counts in int the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (int)</returns>
		public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
		{
			return await readOnlyRepository.CountAsync(filter);
		}

		/// <summary>
		///  Counts in int the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (long)</returns>
		public virtual long LongCount(Expression<Func<TEntity, bool>> filter = null)
		{
			return readOnlyRepository.LongCount(filter);
		}

		/// <summary>
		///  Asynchronously counts in int the entities matching the given filter expression.
		/// </summary>
		/// <param name="filter">Filter expression</param>
		/// <returns>The number of entities matching the filter expression (long)</returns>
		public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> filter = null)
		{
			return await readOnlyRepository.LongCountAsync(filter);
		}

		#endregion
	}
}
