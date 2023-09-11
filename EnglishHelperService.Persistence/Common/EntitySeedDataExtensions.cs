using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Reflection;

namespace EnglishHelperService.Persistence.Common
{
	public static class EntitySeedDataExtensions
	{
		/// <summary>
		/// Creates seed data from a .json file with the same name as the entity type.
		/// Build action: Embedded resource.
		/// It can be used well for all tables where the byte[] type is not handled.
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="builder"></param>
		/// <param name="seedDataContainer">The assembly that contains the seed data (by default the assembly of the calling method)</param>
		public static void Seed<TEntity>(this EntityTypeBuilder<TEntity> builder, Assembly seedDataContainer = null)
				where TEntity : class, new()
		{
			var entityName = typeof(TEntity).Name;

			seedDataContainer = seedDataContainer ?? Assembly.GetCallingAssembly();
			var seedScript = seedDataContainer.GetManifestResourceNames().FirstOrDefault(str => str.EndsWith(entityName + ".json"));

			if (!string.IsNullOrWhiteSpace(seedScript))
			{
				using (var stream = seedDataContainer.GetManifestResourceStream(seedScript))
				using (var reader = new StreamReader(stream))
				{
					string result = reader.ReadToEnd();

					if (!string.IsNullOrWhiteSpace(result))
					{
						var entityList = JsonConvert.DeserializeObject<List<TEntity>>(result);
						builder.HasData(entityList);
					}
				}
			}
		}
	}
}