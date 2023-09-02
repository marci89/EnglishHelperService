using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Reflection;

namespace EnglishHelperService.Persistence.Extensions
{
	public static class EntitySeedDataExtensions
	{
		/// <summary>
		/// Az entity típusával azonos nevű .json fájlból készít seed adatokat. Build action: Embedded resource.
		/// Minden olyan táblánál jól használható, ahol nem kezelünk byte[] típust.
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

		#region User seed because of password hash and salt

		/// <summary>
		/// Olyan User fajtáknál alkalmazható, ahol password hash és salt van jelen - byte[] miatt.
		/// </summary>
		public static List<User> GetPasswordHashSaltUsersSeedData()
		{
			return new List<User>
				{
					new User
						{
						Id = 1,
						Email = "admin@example.com",
						PasswordHash = HexStringToByteArray("0x7AE4D27350FFAED8B036A4D6C4EC4B8CF8BF3B68F48FE81E5245520C6736C4AF6960D61CF33A924BD325A58658384F33B7D62BED3480AFDE0D00A79DD8141859"),
						PasswordSalt = HexStringToByteArray("0x1566D5B4FE8D14B852B0B850C7FF81224CF7D6526963AE57E2DDD6188FDCBC4A2C094C9281DAA127862395D823788245BDBCC10549A4F15987E6883804EBB7D0C8BF5B4998C53E7E12BC44E650C1FA8E3B3D7B195D362E1AF586374B1EDAE29E7E07D80A0082ED33BB8DF0DFBAEE0F62E681049A13668C8612DF3A3B97177ABC"),
						Role = RoleType.Admin,
						Username = "admin"
						}

				};
		}

		/// <summary>
		/// HexString To ByteArray converter
		/// </summary>
		private static byte[] HexStringToByteArray(string hex)
		{
			hex = hex.Replace("0x", "");
			int numBytes = hex.Length / 2;
			byte[] bytes = new byte[numBytes];

			for (int i = 0; i < numBytes; i++)
			{
				bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
			}

			return bytes;
		}


		#endregion
	}
}