using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EnglishHelperService.Persistence
{

    /// <summary>
    /// LearnStatistics datatable configuration class
    /// </summary>
    public class LearnStatisticsConfiguration : IEntityTypeConfiguration<LearnStatistics>
    {
        /// <summary>
        /// LearnStatistics configuration
        /// </summary>
        public void Configure(EntityTypeBuilder<LearnStatistics> builder)
        {
            builder.ToTable("LearnStatistics");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                  .ValueGeneratedOnAdd()
                  .IsRequired();

            builder.Property(u => u.CorrectCount)
                   .IsRequired();

            builder.Property(u => u.IncorrectCount)
                   .IsRequired();


            builder.Property(u => u.Result)
                   .IsRequired();

            builder.Property(u => u.LearnMode)
                   .IsRequired()
                   .HasDefaultValue(LearnModeType.Flashcard)
                   .HasConversion(new EnumToStringConverter<LearnModeType>());

            builder.Property(u => u.Created)
                   .IsRequired();

            builder.HasOne(x => x.User)
             .WithMany(x => x.LearnStatistics)
             .HasForeignKey(c => c.UserId)
             .HasConstraintName("FK_LearnStatistics_UserID")
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
