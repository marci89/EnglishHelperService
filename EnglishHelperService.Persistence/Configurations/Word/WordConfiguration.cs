using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishHelperService.Persistence
{
    /// <summary>
    /// Word datatable configuration class
    /// </summary>
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        /// <summary>
        /// Word datatable configuration
        /// </summary>
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.ToTable("Word");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                  .ValueGeneratedOnAdd()
                  .IsRequired();

            builder.Property(u => u.EnglishText)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.HungarianText)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(u => u.CorrectCount)
                   .IsRequired();

            builder.Property(u => u.IncorrectCount)
                   .IsRequired();

            builder.Property(u => u.Created)
                   .IsRequired();

            builder.Property(u => u.LastUse)
                   .IsRequired(false);

            builder.HasOne(x => x.User)
             .WithMany(x => x.Words)
             .HasForeignKey(c => c.UserId)
             .HasConstraintName("FK_Word_UserID")
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}