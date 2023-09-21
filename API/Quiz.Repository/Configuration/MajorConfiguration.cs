using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class MajorConfiguration : IEntityTypeConfiguration<Major>
    {
        public void Configure(EntityTypeBuilder<Major> builder)
        {
            builder.ToTable("Majors");
            builder.HasKey(e => e.MajorId);
            builder.Property(e => e.MajorId).IsRequired();
            builder.HasOne(x => x.Department).WithMany(x => x.Majors).HasForeignKey(x => x.DepartmentId);
        }
    }
}
