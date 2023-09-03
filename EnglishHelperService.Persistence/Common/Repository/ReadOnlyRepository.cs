using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EnglishHelperService.Persistence.Common
{
	public class ReadOnlyRepository<TDbContext> : IReadOnlyRepository
	   where TDbContext : DbContext
	{

		/// <summary>
		/// Aktuális adatbázis kontextus.
		/// </summary>
		protected TDbContext dbContext;


		/// <summary>
		/// Konstruktor, amely adatbázis kontextust vár.
		/// </summary>
		/// <param name="dbContext">adatbázis kontextus</param>
		public ReadOnlyRepository(TDbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		/// <summary>
		///  Aszinkron módon kiolvassa az adott elsődleges kulccsal rendelkező entitást.
		/// </summary>
		/// <param name="id">elsődleges kulcs</param>
		/// <returns>adott típusú entitás példány, vagy null, ha nem létezik ilyen</returns>
		public virtual async Task<TEntity> ReadAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new()
		{
			return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(where);
		}

		/// <summary>
		///  Készít egy lekérdező objektumot az entitásokhoz.
		/// </summary>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query<TEntity>() where TEntity : class, new()
		{
			return dbContext.Set<TEntity>().AsNoTracking();
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
		{
			return dbContext.Set<TEntity>().Where(filter).AsNoTracking();
		}

		/// <summary>
		///  Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz a megadott sorrendben.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <returns>lekérdező objektum</returns>
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
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, out long totalCount) where TEntity : class, new()
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = dbContext.Set<TEntity>();

			totalCount = baseQuery.LongCount();

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage);
		}

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
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
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
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
		///  Összeszámolja az adott szűrő kifejezésnek megfelelő entitásokat.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>a szűrő kifejezésnek megfelelő entitások száma</returns>
		public virtual long Count<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new()
		{
			var query = dbContext.Set<TEntity>().AsQueryable();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.LongCount();
		}
	}




	/// <summary>
	/// EntityFramework alapú, általános entitás lekérdező osztály, megadott típusú elsődleges kulccsal rendelkező entitások számára.
	/// </summary>
	/// <typeparam name="TDbContext">adatbázis kontextus típusa</typeparam>
	/// <typeparam name="TKey">entitás elsődleges kulcsának típusa</typeparam>
	/// <typeparam name="TEntity">entitás típusa</typeparam>
	public class ReadOnlyRepository<TDbContext, TEntity> : IReadOnlyRepository<TEntity>
		where TDbContext : DbContext
		where TEntity : class, new()
	{
		protected readonly IReadOnlyRepository readOnlyRepository;


		/// <summary>
		/// Aktuális adatbázis kontextus.
		/// </summary>
		protected TDbContext _dbContext;

		/// <summary>
		/// Konstruktor, amely adatbázis kontextust vár.
		/// </summary>
		/// <param name="dbContext">adatbázis kontextus</param>
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
		/// Kiolvassa az adott elsődleges kulccsal rendelkező entitást.
		/// </summary>
		/// <param name="id">elsődleges kulcs</param>
		/// <returns>adott típusú entitás példány, vagy null, ha nem létezik ilyen</returns>
		public virtual async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> where)
		{
			return await readOnlyRepository.ReadAsync(where);
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az entitásokhoz.
		/// </summary>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query()
		{
			return readOnlyRepository.Query<TEntity>();
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
		{
			return readOnlyRepository.Query<TEntity>(filter);
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz a megadott sorrendben.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			return readOnlyRepository.Query<TEntity>(filter, orderBy);
		}

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, out long totalCount)
		{
			return readOnlyRepository.PagedQuery<TEntity>(pageNumber, itemsOnPage, out totalCount);
		}

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount)
		{
			return readOnlyRepository.PagedQuery(pageNumber, itemsOnPage, filter, out totalCount);
		}

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount)
		{
			return readOnlyRepository.PagedQuery(pageNumber, itemsOnPage, filter, orderBy, out totalCount);
		}

		/// <summary>
		/// Összeszámolja az adott szűrő kifejezésnek megfelelő entitásokat.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>a szűrő kifejezésnek megfelelő entitások száma</returns>
		public virtual long Count(Expression<Func<TEntity, bool>> filter = null)
		{
			return readOnlyRepository.Count(filter);
		}
	}
}
