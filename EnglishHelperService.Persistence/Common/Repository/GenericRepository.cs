using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{
	/// <summary>
	/// EntityFramework alapú, általános lekérdező osztály adatbázis entitások számára.
	/// </summary>
	/// <typeparam name="TDbContext">adatbázis kontextus típusa</typeparam>
	/// <typeparam name="TKey">entitás elsődleges kulcsának típusa</typeparam>
	/// <typeparam name="TEntity">entitás típusa</typeparam>
	public class GenericRepository<TDbContext> : ReadOnlyRepository<TDbContext>, IGenericRepository
	where TDbContext : DbContext
	{
		/// <summary>
		/// Konstruktor, amely adatbázis kontextust vár.
		/// </summary>
		/// <param name="dbContext">adatbázis kontextus</param>
		public GenericRepository(TDbContext dbContext) : base(dbContext) { }

		/// <summary>
		/// Aszinkron módon új rekordként menti az átadott entitást.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		public virtual async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class, new()
		{
			dbContext.Add(entity);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Aszinkron módon új rekordokként menti az átadott entitásokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		public virtual async Task CreateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
		{
			dbContext.AddRange(entities);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Aszinkron módon kiolvassa az adott feltételeknek megfelelő első entitást.
		/// </summary>
		/// <param name="where">keresési feltételek</param>
		/// <returns>adott típusú entitás példány, vagy null, ha nem létezik ilyen</returns>
		public override async Task<TEntity> ReadAsync<TEntity>(Expression<Func<TEntity, bool>> where)
		{
			return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(where);
		}

		/// <summary>
		/// Aszinkron módon menti az átadott entitáson történt módosításokat.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		public virtual async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class, new()
		{
			dbContext.Update(entity);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Aszinkron módon új értékeket állít be a feltételeknek megfelelő entitásoknak.
		/// </summary>
		/// <param name="newValues">új értékek</param>
		/// <param name="where">szűrőfeltétel a frissítendő elemekre</param>
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
		///  Aszinkron módon menti az átadott entitásokon történt módosításokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		public virtual async Task UpdateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
		{
			foreach (var entity in entities)
			{
				dbContext.Update(entity);
			}

			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Aszinkron módon törli az adott entitást.
		/// </summary>
		/// <param name="entity">a törlendő entitás</param>
		public virtual async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, new()
		{
			if (entity == null)
				return;

			dbContext.Entry(entity).State = EntityState.Deleted;

			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Aszinkron módon törli az adott feltételeknek megfelelő entitásokat.
		/// </summary>
		/// <param name="where">szűrőfeltétel a törlendő elemekre</param>
		public virtual async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new()
		{
			var entitiesToDelete = where != null
		 ? await dbContext.Set<TEntity>().Where(where).ToListAsync()
		 : await dbContext.Set<TEntity>().ToListAsync();

			dbContext.RemoveRange(entitiesToDelete);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		///  Aszinkron módon törli az adott entitásokat.
		/// </summary>
		/// <param name="entities">a törlendő entitások</param>
		public virtual async Task DeleteManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
		{
			dbContext.RemoveRange(entities);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az entitásokhoz.
		/// </summary>
		/// <returns>lekérdező objektum</returns>
		public override IQueryable<TEntity> Query<TEntity>()
		{
			var baseQuery = dbContext.Set<TEntity>();

			return baseQuery.AsNoTracking();
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		public override IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter)
		{
			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			return baseQuery.AsNoTracking();
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz a megadott sorrendben.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		public override IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			baseQuery = orderBy == null ? baseQuery : orderBy(baseQuery);

			return baseQuery.AsNoTracking();
		}

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		public override IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, out long totalCount)
		{
			pageNumber = pageNumber < 1 ? 0 : (pageNumber - 1);

			var baseQuery = dbContext.Set<TEntity>();

			totalCount = baseQuery.LongCount();

			return baseQuery.Skip(pageNumber * itemsOnPage).Take(itemsOnPage).AsNoTracking();
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
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
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
		/// Összeszámolja az adott szűrő kifejezésnek megfelelő entitásokat.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>a szűrő kifejezésnek megfelelő entitások száma</returns>
		public override long Count<TEntity>(Expression<Func<TEntity, bool>> filter = null)
		{
			var baseQuery = filter == null
				? dbContext.Set<TEntity>()
				: dbContext.Set<TEntity>().Where(filter);

			return baseQuery.LongCount();
		}

		/// <summary>
		///  Aszinkron módon menti az entitás(ok)on történt módosítás(oka)t.
		/// </summary>
		public virtual async Task SaveAsync()
		{
			await dbContext.SaveChangesAsync();
		}
	}

	/// <summary>
	/// EntityFramework alapú, általános entitás lekérdező osztály, megadott típusú elsődleges kulccsal rendelkező entitások számára.
	/// </summary>
	/// <typeparam name="TDbContext">adatbázis kontextus típusa</typeparam>
	/// <typeparam name="TKey">entitás elsődleges kulcsának típusa</typeparam>
	/// <typeparam name="TEntity">entitás típusa</typeparam>
	public class GenericRepository<TDbContext, TEntity> : IRepository<TEntity>
		where TDbContext : DbContext
		where TEntity : class, new()
	{
		private readonly IGenericRepository _genericRepository;

		/// <summary>
		/// Aktuális adatbázis kontextus.
		/// </summary>
		protected TDbContext dbContext;

		/// <summary>
		/// Konstruktor, amely adatbázis kontextust vár.
		/// </summary>
		/// <param name="dbContext">adatbázis kontextus</param>
		public GenericRepository(TDbContext dbContext)
		{
			dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_genericRepository = new GenericRepository<TDbContext>(dbContext);
		}

		/// <summary>
		/// A lekérdezhető teljes entitás halmaz.
		/// </summary>
		public virtual IQueryable<TEntity> EntitySet => dbContext.Set<TEntity>().AsNoTracking();

		/// <summary>
		///  Aszinkron módon új rekordként menti az átadott entitást.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		public virtual async Task CreateAsync(TEntity entity)
		{
			await _genericRepository.CreateAsync(entity);
		}

		/// <summary>
		///  Aszinkron módon új rekordokként menti az átadott entitásokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		public virtual async Task CreateManyAsync(IEnumerable<TEntity> entities)
		{
			await _genericRepository.CreateManyAsync(entities);
		}

		/// <summary>
		///  Aszinkron módon kiolvassa az adott elsődleges kulccsal rendelkező entitást.
		/// </summary>
		/// <param name="id">elsődleges kulcs</param>
		/// <returns>adott típusú entitás példány, vagy null, ha nem létezik ilyen</returns>
		public virtual async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> where)
		{
			return await _genericRepository.ReadAsync(where);
		}

		/// <summary>
		///  Aszinkron módon menti az átadott entitáson történt módosításokat.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		public virtual async Task UpdateAsync(TEntity entity)
		{
			await _genericRepository.UpdateAsync(entity);
		}

		/// <summary>
		///  Aszinkron módon új értékeket állít be a feltételeknek megfelelő entitásoknak.
		/// </summary>
		/// <param name="newValues">új értékek</param>
		/// <param name="where">szűrőfeltétel a frissítendő elemekre</param>
		public virtual async Task UpdateAsync(Expression<Func<TEntity, TEntity>> newValues, Expression<Func<TEntity, bool>> where = null)
		{
			await _genericRepository.UpdateAsync(newValues, where);
		}

		/// <summary>
		///  Aszinkron módon menti az átadott entitásokon történt módosításokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities)
		{
			await _genericRepository.UpdateManyAsync(entities);
		}

		/// <summary>
		///  Aszinkron módon törli az adott entitást.
		/// </summary>
		/// <param name="entity">a törlendő entitás</param>
		public virtual async Task DeleteAsync(TEntity entity)
		{
			await _genericRepository.DeleteAsync(entity);
		}

		/// <summary>
		///  Aszinkron módon törli az adott feltételeknek megfelelő entitásokat.
		/// </summary>
		/// <param name="where">szűrőfeltétel a törlendő elemekre</param>
		public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> where = null)
		{
			await _genericRepository.DeleteAsync(where);
		}

		/// <summary>
		///  Aszinkron módon törli az adott entitásokat.
		/// </summary>
		/// <param name="entities">a törlendő entitások</param>
		public virtual async Task DeleteManyAsync(IEnumerable<TEntity> entities)
		{
			await _genericRepository.DeleteManyAsync(entities);
		}

		/// <summary>
		///  Készít egy lekérdező objektumot az entitásokhoz.
		/// </summary>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query()
		{
			return _genericRepository.Query<TEntity>();
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
		{
			return _genericRepository.Query(filter);
		}

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz a megadott sorrendben.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			return _genericRepository.Query(filter, orderBy);
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
			return _genericRepository.PagedQuery<TEntity>(pageNumber, itemsOnPage, out totalCount);
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
			return _genericRepository.PagedQuery(pageNumber, itemsOnPage, filter, out totalCount);
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
			return _genericRepository.PagedQuery(pageNumber, itemsOnPage, filter, orderBy, out totalCount);
		}

		/// <summary>
		/// Összeszámolja az adott szűrő kifejezésnek megfelelő entitásokat.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>a szűrő kifejezésnek megfelelő entitások száma</returns>
		public virtual long Count(Expression<Func<TEntity, bool>> filter = null)
		{
			return _genericRepository.Count(filter);
		}

		/// <summary>
		///  Aszinkron módon Menti az entitás(ok)on történt módosítás(oka)t.
		/// </summary>
		public virtual async Task SaveAsync()
		{
			await dbContext.SaveChangesAsync();
		}
	}
}