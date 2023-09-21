using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(x => x.QuestionId);
            builder.Property(x => x.QuestionId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.QuestionText).IsRequired();
            builder.Property(x => x.QuestionA).IsRequired();
            builder.Property(x => x.QuestionB).IsRequired();
            builder.Property(x => x.QuestionC).IsRequired();
            builder.Property(x => x.QuestionD).IsRequired();
            builder.Property(x => x.Answer).IsRequired();
            builder.HasOne(x => x.Module).WithMany(x => x.Questions).HasForeignKey(x => x.ModuleId);
        }
    }
}
