﻿using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

			builder.Property(u => u.Username)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.HasIndex(u => u.Username)
			   .IsUnique(true);


			builder.Property(u => u.PasswordHash)
				   .IsRequired();

			builder.Property(u => u.PasswordSalt)
				   .IsRequired();

			builder.Property(u => u.Email)
				   .IsRequired()
				   .HasMaxLength(250);

			builder.HasIndex(u => u.Email)
				   .IsUnique(true);

			builder.Property(u => u.Created)
				   .IsRequired();

		}
	}
}