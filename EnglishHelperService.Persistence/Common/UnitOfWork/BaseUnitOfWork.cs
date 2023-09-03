using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnglishHelperService.Persistence.Common
{
	public class BaseUnitOfWork<TDbContext> : IBaseUnitOfWork where TDbContext : DbContext
	{
		private IDbContextTransaction _transaction = null;
		protected TDbContext dbContext { get; }

		public BaseUnitOfWork(TDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Adatbázis tranzakciót indít.
		/// </summary>
		public void BeginTransaction()
		{
			if (_transaction == null)
				_transaction = dbContext.Database.BeginTransaction();
		}

		/// <summary>
		/// Menti a módosításokat az adatbázisba, de nem véglegesít (nem commitol).
		/// </summary>
		public async Task SaveAsync()
		{
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Véglegesíti a módosításokat az adatbázison.
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
		/// Visszagörgeti a módosításokat az adatbázison.
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
