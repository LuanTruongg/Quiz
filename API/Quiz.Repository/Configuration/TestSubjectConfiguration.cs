using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class TestSubjectConfiguration : IEntityTypeConfiguration<TestSubject>
    {
        public void Configure(EntityTypeBuilder<TestSubject> builder)
        {
            builder.ToTable("TestSubjects");
            builder.HasKey(x => x.TestSubjectId);
            builder.Property(x => x.TestSubjectId).IsRequired();
            builder.HasOne(x => x.TestStructure).WithMany(x => x.TestSubjects).HasForeignKey(x => x.TestStructureId);
            builder.HasOne(x => x.Question).WithMany(x => x.TestSubjects).HasForeignKey(x => x.QuestionId);
        }
    }
}
