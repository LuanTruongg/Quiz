using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class MajorSubjectConfiguration : IEntityTypeConfiguration<MajorSubject>
    {
        public void Configure(EntityTypeBuilder<MajorSubject> builder)
        {
            builder.ToTable("MajorSubjects");
            builder.HasKey(x => new { x.MajorId, x.SubjectId });
            builder.HasOne(x => x.Major).WithMany(x => x.MajorSubjects).HasForeignKey(x => x.MajorId);
            builder.HasOne(x => x.Subject).WithMany(x => x.MajorSubjects).HasForeignKey(x => x.SubjectId);
        }
    }
}
