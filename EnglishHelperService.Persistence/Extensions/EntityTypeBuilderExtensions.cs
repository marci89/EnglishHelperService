using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Reflection;

namespace EnglishHelperService.Persistence.Extensions
{
	public static class EntityTypeBuilderExtensions
	{
		/// <summary>
		/// Az entity típusával azonos nevű .json fájlból készít seed adatokat.
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="builder"></param>
		/// <param name="seedDataContainer">Az az assembly, amely az seed adatokat tartalmazza (alapértelmezetten a hívó metódus assembly-je)</param>
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