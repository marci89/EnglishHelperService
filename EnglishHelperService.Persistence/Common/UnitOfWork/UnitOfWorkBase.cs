using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnglishHelperService.Persistence.Common
{
	/// <summary>
	/// class describing the integration of database connection services.
	/// </summary>
	public class UnitOfWorkBase<TDbContext> : IUnitOfWorkBase where TDbContext : DbContext
	{
		private IDbContextTransaction _transaction = null;
		protected TDbContext dbContext { get; }

		public UnitOfWorkBase(TDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Starts a database transaction.
		/// </summary>
		public void BeginTransaction()
		{
			if (_transaction == null)
				_transaction = dbContext.Database.BeginTransaction();
		}

		/// <summary>
		/// It saves the changes made up to that point in the database, but you have not committed a transaction.
		/// </summary>
		public async Task SaveAsync()
		{
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Finalizes changes to the database (saved and committed).
		/// </summary>
		public async Task CommitAsync()
		{
			await dbContext.SaveChangesAsync();
			if (_transaction != null)
			{
				await _transaction.CommitAsync();
				_transaction.Dispose();
				_transaction = null;
			}
		}

		/// <summary>
		/// Rolls back changes on the database.
		/// </summary>
		public void Rollback()
		{
			if (_transaction != null)
			{
				_transaction.Rollback();
				_transaction.Dispose();
				_transaction = null;
			}
			dbContext.ChangeTracker
				.Entries()
				.ToList()
				.ForEach(e => e.Reload());
		}


		#region IDisposable Support

		// To detect redundant calls
		private bool _disposedValue = false;

		public async Task DisposeAsync(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					if (_transaction != null)
						_transaction.Dispose();

					await dbContext.DisposeAsync();
				}

				_disposedValue = true;
			}
		}


		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			DisposeAsync(true).GetAwaiter().GetResult();
		}

		#endregion
	}
}
