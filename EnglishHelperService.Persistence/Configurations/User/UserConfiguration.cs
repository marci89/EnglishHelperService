using EnglishHelperService.Persistence.Entities;
using EnglishHelperService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EnglishHelperService.Persistence
{
	/// <summary>
	/// User datatable configuration class
	/// </summary>
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		/// <summary>
		/// User datatable configuration
		/// </summary>
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("User");

			builder.HasKey(u => u.Id);

			builder.Property(u => u.Id)
				  .ValueGeneratedOnAdd()
				  .IsRequired();

			builder.Property(u => u.Role)
		   .IsRequired()
		   .HasDefaultValue(RoleType.Member)
		   .HasConversion(new EnumToStringConverter<RoleType>());

			builder.Property(u => u.Username)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.HasIndex(u => u.Username)
				   .IsUnique(true);


			builder.Property(u => u.Password)
				   .IsRequired();

			builder.Property(u => u.Email)
				   .IsRequired()
				   .HasMaxLength(250);

			builder.HasIndex(u => u.Email)
				   .IsUnique(true);

			builder.Property(u => u.Created)
				   .HasDefaultValue(DateTime.UtcNow)
				   .IsRequired();

			builder.Property(u => u.LastActive)
				   .HasDefaultValue(DateTime.UtcNow)
				   .IsRequired();

			builder.Seed();

		}
	}
}