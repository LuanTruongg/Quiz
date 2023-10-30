using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.ToTable("UserAnswers");
            builder.HasKey(x => x.UserAnswerId);
            builder.Property(x => x.UserAnswerId).IsRequired().ValueGeneratedOnAdd();
            builder.HasOne(x => x.UserTest).WithMany(x => x.UserAnswers).HasForeignKey(x => x.UserTestId);
        }
    }
}
