using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnglishHelperService.Persistence.UnitOfWork
{
	public class BaseUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
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
		public void Save()
		{
			dbContext.SaveChanges();
		}

		/// <summary>
		/// Véglegesíti a módosításokat az adatbázison.
		/// </summary>
		public void Commit()
		{
			dbContext.SaveChanges();
			if (_transaction != null)
			{
				_transaction.Commit();
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

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					if (_transaction != null)
						_transaction.Dispose();

					dbContext.Dispose();
				}

				_disposedValue = true;
			}
		}


		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// Uncomment the following line if the finalizer is overridden above.
			//GC.SuppressFinalize(this);
		}

		#endregion
	}
}
