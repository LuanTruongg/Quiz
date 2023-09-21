using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class TestStructureConfiguration : IEntityTypeConfiguration<TestStructure>
    {
        public void Configure(EntityTypeBuilder<TestStructure> builder)
        {
            builder.ToTable("TestStructures");
            builder.HasKey(x => x.TestStructureId);
            builder.Property(x => x.TestStructureId).IsRequired();
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.NumberOfQuestions).IsRequired();
        }
    }
}
